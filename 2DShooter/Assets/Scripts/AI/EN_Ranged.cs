/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class EN_Ranged : EN_Base
{

    public PJ_Bolt                      PF_Bolt;

    private float                       _lastShot;
    public float                        _shotInterval = 3f;

    public GameObject                   rFirePoint;

    void Update()
    {
        // rotate towards the player.
        Vector3 vDir = rPC.transform.position - transform.position;
        vDir = Vector3.Normalize(vDir);
        float angle = Mathf.Atan2(vDir.y, vDir.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.eulerAngles = new Vector3(0,0,angle);

        if(Time.time - _lastShot > _shotInterval)
        {
            PJ_Bolt b = Instantiate(PF_Bolt, rFirePoint.transform.position, transform.rotation);
            vDir = rPC.transform.position - rFirePoint.transform.position;
            b.FFireDirection(vDir);
            _lastShot = Time.time;
        }
    }
}
