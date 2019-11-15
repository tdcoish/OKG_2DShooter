/*************************************************************************************
Zerg rush the player.

Okay update, now they are going to rush towards the player, then they charge when they 
get close.
*************************************************************************************/
using UnityEngine;

public class EN_Melee : EN_Base
{

    public enum STATE{
        S_TRACKING,
        S_CHARGING,
        S_RECOVERING
    }
    public STATE                        _state;

    private float                       _chargeStartTime;
    public float                        _maxChargeDis = 2f;
    public float                        _chargeSpdBoost = 3f;
    public float                        _chargeTime = 2f;

    private float                       _recoveryStartTime;
    public float                        _recoveryTime = 1f;

    void Start()
    {
        base.Start();
        _state = STATE.S_TRACKING;
    }

    void Update()
    {

        switch(_state){
            case STATE.S_TRACKING: RUN_TRACKING(); break;
            case STATE.S_CHARGING: RUN_CHARGING(); break;
            case STATE.S_RECOVERING: RUN_RECOVERING(); break;
        }

        if(_health <= 0f){
            KillYourself();
        }
    }

    private void ENTER_TRACKING(){
        _state = STATE.S_TRACKING;
    }
    private void RUN_TRACKING()
    {
        // if they can see the player, then just move to him. If not, you gotta use pathfinding.
        bool canSeePC = cSeePC.FCanSeePlayer(rPC.transform.position);
        if(!canSeePC)
        {
            Debug.Log("Can't see him");
            _pathList = cPath.FFindPath(transform.position, rPC.transform.position);
            if(_pathList == null || _pathList.Count == 0)
            {
                Debug.Log("No nodes or null path");
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
            Debug.Log("Straight shooting");
            Vector2 vDif = rPC.transform.position - transform.position;
            vDif = Vector3.Normalize(vDif);
            cRigid.velocity = vDif * _spd;
        }

        if(Vector3.Distance(transform.position, rPC.transform.position) < _maxChargeDis){
            ENTER_CHARGING();
        }
    }
    private void ENTER_CHARGING(){
        _state = STATE.S_CHARGING;
        cRigid.velocity *= 3f;
        _chargeStartTime = Time.time;
    }
    private void RUN_CHARGING()
    {
        if(Time.time - _chargeStartTime > _chargeTime){
            ENTER_RECOVERING();
        }
    }
    private void ENTER_RECOVERING()
    {
        _state = STATE.S_RECOVERING;
        _recoveryStartTime = Time.time;
        cRigid.velocity = Vector3.zero;
    }
    private void RUN_RECOVERING()
    {
        if(Time.time - _recoveryStartTime > _recoveryTime){
            ENTER_TRACKING();
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
        if(other.GetComponent<EN_Melee>()){
            _health = 0f;
        }
    }

}
