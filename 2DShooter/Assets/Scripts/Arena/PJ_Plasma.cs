/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Plasma : PJ_Base
{
    public ParticleSystem                           PF_WallHit;

    void Update()
    {
        if(cLifetime._lifeOver){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EN_Melee>())
        {
            Destroy(gameObject);
        }
        if(other.GetComponent<ENV_Wall>())
        {
            Instantiate(PF_WallHit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
