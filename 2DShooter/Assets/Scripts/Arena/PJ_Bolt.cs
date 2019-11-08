/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Bolt : PJ_Base
{

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.GetComponent<EN_Melee>()){
        //     Destroy(gameObject);
        // }

        if(other.GetComponent<PC_Cont>()){
            Destroy(gameObject);
        }
    }
}
