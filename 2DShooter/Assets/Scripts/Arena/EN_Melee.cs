/*************************************************************************************
Zerg rush the player.
*************************************************************************************/
using UnityEngine;

public class EN_Melee : EN_Base
{

    void Update()
    {
        Vector2 vDif = rPC.transform.position - transform.position;
        vDif = Vector3.Normalize(vDif);
        cRigid.velocity = vDif * _spd;

        if(_health <= 0f){
            KillYourself();
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
    }

}
