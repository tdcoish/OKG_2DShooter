/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PCK_Health : MonoBehaviour
{
    public ParticleSystem                       PF_Particles;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PC_Cont>()){
            Instantiate(PF_Particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
