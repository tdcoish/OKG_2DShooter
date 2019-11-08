/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Base : MonoBehaviour
{
    protected Rigidbody2D                           cRigid;
    protected UT_LifeTime                           cLifetime;

    public float                                    _spd;

    void Awake()
    {
        cLifetime = GetComponent<UT_LifeTime>();   
        cRigid = GetComponent<Rigidbody2D>();    
    }

    public void FFireDirection(Vector3 vDir)
    {
        vDir = Vector3.Normalize(vDir);
        vDir *= _spd;
        cRigid.velocity = vDir;
    }
}
