/************************************************************
************************************************************/
using UnityEngine;

public class PJ_Snipe : PJ_Base
{

    public ParticleSystem                               PF_DeathParticles;

    void Update()
    {
        // make him rotate to the direction he is moving.
        Vector3 vDir = cRigid.velocity.normalized;
        vDir = Vector3.Normalize(vDir);
        float angle = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.eulerAngles = new Vector3(0,0,angle);

        if(cLifetime._lifeOver){
            Destroy(gameObject);
        }
    }
    
    private void KillYourself()
    {
        Instantiate(PF_DeathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.GetComponent<EN_Melee>()){
        //     Destroy(gameObject);
        // }

        if(other.GetComponent<ENV_Wall>()){
            KillYourself();
        }

        if(other.GetComponent<PC_Cont>()){
            KillYourself();
        }
    }
}
