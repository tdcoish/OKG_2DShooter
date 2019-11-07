/*************************************************************************************
For now it just explodes after x seconds. 
*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PJ_Grenade : MonoBehaviour
{
    private Rigidbody2D                 cRigid;
    public EX_Grenade                   PF_Explosion;
    public float                        _spd = 3f;

    void Awake()
    {
        cRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(GetComponent<UT_LifeTime>()._lifeOver){
            Instantiate(PF_Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void FFireDirection(Vector3 vDir)
    {
        vDir = Vector3.Normalize(vDir);
        vDir *= _spd;
        cRigid.velocity = vDir;
    }
}
