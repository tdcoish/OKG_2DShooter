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

    public float                        _dropAmmoChance = 10f;
    public float                        _dropHealthChance = 10f;
    public PCK_Ammo                     PF_AmmoBox;
    public PCK_Health                   PF_HealthBox;
 
 
    protected void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
        rPC = FindObjectOfType<PC_Cont>();
    }

    protected virtual void KillYourself()
    {
        Instantiate(PF_Explode, transform.position, transform.rotation);
        Instantiate(PF_Gibs, transform.position, transform.rotation);

        // Randomly decide to spawn in ammo or health.
        {
            float random = Random.Range(0, 100f);
            if(random < _dropAmmoChance){
                Instantiate(PF_AmmoBox, transform.position, transform.rotation);
            }else{
                random = Random.Range(0, 100f);
                if(random < _dropHealthChance){
                    Instantiate(PF_HealthBox, transform.position, transform.rotation);
                }
            }
        }

        TDC_EventManager.FBroadcast(TDC_GE.GE_EDeath);

        Destroy(gameObject);
    }
}
