/*************************************************************************************
Zerg rush the player.
*************************************************************************************/
using UnityEngine;

public class EN_Melee : MonoBehaviour
{
    private Rigidbody2D                 cRigid;

    public float                        _spd;
    PC_Cont                             rPC;

    public float                        _health = 100f;

    public ParticleSystem               PF_HitByBullet;
    public ParticleSystem               PF_Explode;
    public GFX_Gibs                     PF_Gibs;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
        rPC = FindObjectOfType<PC_Cont>();
    }

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

    private void KillYourself()
    {
        Instantiate(PF_Explode, transform.position, transform.rotation);
        Instantiate(PF_Gibs, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
