/*************************************************************************************

*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(UT_LifeTime))]
public class GFX_Gibs : MonoBehaviour
{

    public GFX_Giblet                       _femur;
    public GFX_Giblet                       _skull;
    public GFX_Giblet                       _pelvis;

    // Scatter the gibs to the wind
    void Start()
    {
        Vector3 vVel = new Vector3();
        vVel.x = -0.2f;   vVel.y = 0.4f;
        _femur.GetComponent<Rigidbody2D>().velocity = vVel;
        vVel.x = 0.8f; vVel.y = 0f;
        _pelvis.GetComponent<Rigidbody2D>().velocity = vVel;
        vVel.x = 0f; vVel.y = 1.2f;
        _skull.GetComponent<Rigidbody2D>().velocity = vVel;
    }

    void Update()
    {
        if(GetComponent<UT_LifeTime>()._lifeOver){
            Destroy(gameObject);
        }
    }
}
