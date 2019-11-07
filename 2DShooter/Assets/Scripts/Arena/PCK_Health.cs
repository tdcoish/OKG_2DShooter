/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PCK_Health : MonoBehaviour
{
    public ParticleSystem                       PF_Particles;

    private float                               _spawnTime;
    public float                                _lifeTime = 2f;

    void Start()
    {
        _spawnTime = Time.time;
    }
    void Update()
    {
        if(Time.time - _spawnTime > _lifeTime){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PC_Cont>()){
            Instantiate(PF_Particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
