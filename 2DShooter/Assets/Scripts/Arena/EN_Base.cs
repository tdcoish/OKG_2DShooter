/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class EN_Base : MonoBehaviour
{
    protected Rigidbody2D               cRigid;

    public float                        _spd;
    protected PC_Cont                   rPC;

    public float                        _health = 100f;

    public ParticleSystem               PF_HitByBullet;
    public ParticleSystem               PF_Explode;
    public GFX_Gibs                     PF_Gibs;
 
 
    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
        rPC = FindObjectOfType<PC_Cont>();
    }

    protected virtual void KillYourself()
    {
        Instantiate(PF_Explode, transform.position, transform.rotation);
        Instantiate(PF_Gibs, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
