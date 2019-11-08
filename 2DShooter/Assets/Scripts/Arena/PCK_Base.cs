/*************************************************************************************

*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(UT_LifeTime))]
public class PCK_Base : MonoBehaviour
{

    public ParticleSystem                       PF_Particles;

    protected virtual void Update()
    {
        if(GetComponent<UT_LifeTime>()._lifeOver){
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PC_Cont>()){
            Instantiate(PF_Particles, transform.position, transform.rotation);
            PickupEvent();
            Destroy(gameObject);
        }
    }

    protected virtual void PickupEvent()
    {

    }
}
