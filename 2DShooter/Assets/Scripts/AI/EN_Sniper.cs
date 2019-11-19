/************************************************************
The sniper shuffles his way across the map. When he can see the player,
he starts charging up his little rifle, before shooting a totally 
unavoidable charge. This charge will not damage the player if the 
player is behind something that blocks the shot, like a wall.

Need to figure out a way to get a laser drawn.
************************************************************/
using UnityEngine;

public class EN_Sniper : EN_Base
{

    public enum STATE
    {
        S_Tracking,
        S_Charging,
        S_Recovering
    }
    public STATE                                _state;

    public LineRenderer                         _laser;

    private float                               _stateChangeTime;
    public float                                _chargeInterval;
    public float                                _recoverInterval;

    public PJ_Snipe                             PF_SnipeShot;

    void Start()
    {
        base.Start();
        _laser.enabled = false;
    }


    void Update()
    {
        switch(_state){
            case STATE.S_Tracking: RUN_Tracking();break;
            case STATE.S_Charging: RUN_Charging(); break;
            case STATE.S_Recovering: RUN_Recovering(); break;
        }
    }

    private void ENTER_Tracking(){
        _state = STATE.S_Tracking;
    }
    private void RUN_Tracking()
    {
        // if they can see the player, then just move to him. If not, you gotta use pathfinding.
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        if(!canSeePC)
        {
            _pathList = cPath.FFindPath(transform.position, rPC.transform.position);
            if(_pathList == null || _pathList.Count == 0)
            {
                return;
            }

            Vector3 vDir = _pathList[0].transform.position - transform.position;
            cRigid.velocity = Vector3.Normalize(vDir) * _spd;

            for(int i=1; i<_pathList.Count; i++){
                Debug.DrawLine(_pathList[i].transform.position, _pathList[i-1].transform.position);
            }
        }
        else
        {
            ENTER_Charging();
        }
    }
    private void ENTER_Charging(){
        _state = STATE.S_Charging;
        _stateChangeTime = Time.time;
        _laser.enabled = true;
    }
    private void RUN_Charging()
    {
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        if(!canSeePC){
            EXIT_Charging();
            ENTER_Tracking();
            return;
        }

        _laser.positionCount = 2;
        _laser.SetPosition(0, transform.position);
        _laser.SetPosition(1, rPC.transform.position);

        if(Time.time - _stateChangeTime > _chargeInterval){
            PJ_Snipe s = Instantiate(PF_SnipeShot, transform.position, transform.rotation);
            s.FFireDirection(Vector3.Normalize(rPC.transform.position - transform.position));
            Debug.Log("Fired Snipe Shot");
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
            ENTER_Tracking();
        }
    }
}
