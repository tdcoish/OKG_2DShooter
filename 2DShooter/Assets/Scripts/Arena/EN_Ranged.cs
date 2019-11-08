/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class EN_Ranged : EN_Base
{

    public PJ_Bolt                      PF_Bolt;

    private float                       _lastShot;
    public float                        _shotInterval = 3f;

    void Update()
    {
        if(Time.time - _lastShot > _shotInterval)
        {
            PJ_Bolt b = Instantiate(PF_Bolt, transform.position, transform.rotation);
            Vector3 vDir = rPC.transform.position - transform.position;
            b.FFireDirection(vDir);
            _lastShot = Time.time;
        }
    }
}
