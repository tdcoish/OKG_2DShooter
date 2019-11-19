/************************************************************
The AI for the security guard.

old pathfinding code:
        if(fDis > _visionDistance || !canSeePC){
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


Really, they should be moving to a spot where they can fire at the player.
************************************************************/
using UnityEngine;

public class EN_SecGuard : EN_Base
{

    public enum STATE
    {
        S_Tracking,
        S_Aiming,
        S_Firing,
        S_Recovering
    }
    public STATE                        _state;

    private float                       _stateChangeTime;

    public float                        _fireInterval = 3f;
    private float                       _lastFireTime;

    public float                        _aimInterval = 1f;
    public float                        _fireTime = 0.5f;
    public float                        _recoverTime = 1f;

    public PJ_Bolt                      PF_Bolt;

    void Start()
    {
        base.Start();
        ENTER_Tracking();
    }

    void Update()
    {
        switch(_state)
        {
            case STATE.S_Tracking: RUN_Tracking(); break;
            case STATE.S_Aiming: RUN_Aiming(); break;
            case STATE.S_Firing: RUN_Firing(); break;
            case STATE.S_Recovering: RUN_Recovering(); break;
        }

        if(_health <= 0f){
            KillYourself();
        }
    }

    private void ENTER_Tracking(){
        _state = STATE.S_Tracking;
    }
    // If the player is too far away, then we keep tracking them. If not, then we do our attack stuffs.
    private void RUN_Tracking()
    {
        // Move towards the player.
        Vector3 vDir = rPC.transform.position - transform.position;
        vDir = Vector3.Normalize(vDir);
        cRigid.velocity = vDir * _spd;

        float fDis = Vector3.Distance(rPC.transform.position, transform.position);
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        if(fDis < _visionDistance && canSeePC){
            ENTER_Aiming();
        }

    }

    private void ENTER_Aiming(){
        cRigid.velocity = Vector3.zero;
        Debug.Log("Aiming Now");
        _state = STATE.S_Aiming;
        _stateChangeTime = Time.time;

    }
    private void RUN_Aiming()
    {
        if(Time.time - _stateChangeTime > _aimInterval)
        {
            ENTER_Firing();
        }
    }

    private void ENTER_Firing(){
        Debug.Log("Firing Now");
        _state = STATE.S_Firing;
        _stateChangeTime = Time.time;

        PJ_Bolt b = Instantiate(PF_Bolt, transform.position, transform.rotation);
        b.FFireDirection(Vector3.Normalize(rPC.transform.position - transform.position));
    }
    private void RUN_Firing()
    {
        if(Time.time - _stateChangeTime > _aimInterval)
        {
            ENTER_Recovering();
        }  
    }

    private void ENTER_Recovering(){
        Debug.Log("Recovering Now");
        _state = STATE.S_Recovering;        
        _stateChangeTime = Time.time;
    }
    private void RUN_Recovering()
    {
        if(Time.time - _stateChangeTime > _aimInterval)
        {
            ENTER_Tracking();
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
