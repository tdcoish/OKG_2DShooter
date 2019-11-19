/************************************************************
The sniper shuffles his way across the map. When he can see the player,
he starts charging up his little rifle, before shooting a totally 
unavoidable charge. This charge will not damage the player if the 
player is behind something that blocks the shot, like a wall.

Need to figure out a way to get a laser drawn. Done.

Now I need them to sort of shuffle around when they can't see the player.
Gives them the illusion of intellect.
************************************************************/
using UnityEngine;

public class EN_Sniper : EN_Base
{
    private AD_Sniper                          cAud;

    public enum STATE
    {
        S_IdleMove,
        S_Charging,
        S_Recovering
    }
    public STATE                                _state;

    public LineRenderer                         _laser;

    private float                               _stateChangeTime;
    public float                                _chargeInterval;
    public float                                _recoverInterval;

    public float                                _idleMoveInterval = 3f;
    private float                               _lastIdleMoveTime;
    private Vector3                             _idleMoveSpot;

    public float                                _rotPerSec = 15f;

    public PJ_Snipe                             PF_SnipeShot;

    void Start()
    {
        base.Start();
        cAud = GetComponentInChildren<AD_Sniper>();
        _laser.enabled = false;
        _lastIdleMoveTime = _idleMoveInterval*-1f;
    }


    void Update()
    {
        switch(_state){
            case STATE.S_IdleMove: RUN_IdleMoving();break;
            case STATE.S_Charging: RUN_Charging(); break;
            case STATE.S_Recovering: RUN_Recovering(); break;
        }

        if(_health <= 0f){
            KillYourself();
        }
    }

    private void ENTER_IdleMoving(){
        _state = STATE.S_IdleMove;
    }
    // The snipers just try to move back and forth. For now just have them getting to a spot that's to the left of them?
    private void RUN_IdleMoving()
    {
        // figure out if we need a new idle movement spot. Something really close to where we currently are.
        if(Time.time - _lastIdleMoveTime >= _idleMoveInterval){
            _lastIdleMoveTime = Time.time;
            _idleMoveSpot = transform.position;
            _idleMoveSpot.x += Random.Range(-3f, 3f);
            _idleMoveSpot.y += Random.Range(-3f, 3f);
        }

        // just sort of aimlessly pick a spot near you and slowly move there.
        Vector3 vDir = Vector3.Normalize(_idleMoveSpot - transform.position);
        cRigid.velocity = vDir * _spd;

        // if they can see the player, then just move to him. If not, you gotta use pathfinding.
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        float fDisToPlayer = Vector3.Distance(rPC.transform.position, transform.position);
        if(fDisToPlayer < _visionDistance && canSeePC){
            ENTER_Charging();
        }
        
    }
    private void ENTER_Charging(){
        cRigid.velocity = Vector3.zero;
        _state = STATE.S_Charging;
        _stateChangeTime = Time.time;
        _laser.enabled = true;
    }
    private void RUN_Charging()
    {
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        if(!canSeePC){
            EXIT_Charging();
            ENTER_IdleMoving();
            return;
        }

        _laser.positionCount = 2;
        _laser.SetPosition(0, transform.position);
        _laser.SetPosition(1, rPC.transform.position);

        // Now we have shoot out three projectiles in a semi-tight burst.
        if(Time.time - _stateChangeTime > _chargeInterval){
            Vector3 vDir = Vector3.Normalize(rPC.transform.position - transform.position);
            Vector3 vRight = Vector3.Cross(vDir, Vector3.forward);
            PJ_Snipe s1 = Instantiate(PF_SnipeShot, transform.position, transform.rotation);
            s1.FFireDirection(Vector3.Normalize(vDir + 0.2f*vRight));
            PJ_Snipe s2 = Instantiate(PF_SnipeShot, transform.position, transform.rotation);
            s2.FFireDirection(Vector3.Normalize(vDir));
            PJ_Snipe s3 = Instantiate(PF_SnipeShot, transform.position, transform.rotation);
            s3.FFireDirection(Vector3.Normalize(vDir - 0.2f*vRight));
            cAud.FPlayFire();

            EXIT_Charging();
            ENTER_Recovering();
        }
    }
    private void EXIT_Charging(){
        _laser.enabled = false;
    }
    private void ENTER_Recovering(){
        _state = STATE.S_Recovering;
        _stateChangeTime = Time.time;
    }
    private void RUN_Recovering()
    {
        if(Time.time - _stateChangeTime > _recoverInterval){
            ENTER_IdleMoving();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PJ_Plasma>()){
            _health -= 50f;
            Instantiate(PF_HitByBullet, transform.position, transform.rotation);
        }
        if(other.GetComponent<PC_Cont>()){
            _health = 0f;
        }
        if(other.GetComponent<EX_Grenade>()){
            _health = 0f;
        }
        // if(other.GetComponent<EN_Melee>()){
        //     _health = 0f;
        // }
    }
}
