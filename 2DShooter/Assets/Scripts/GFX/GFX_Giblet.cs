/*************************************************************************************

*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GFX_Giblet : MonoBehaviour
{
    private Rigidbody2D                         cRigid;
    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 vVel = cRigid.velocity;
        vVel *= 0.98f;
        cRigid.velocity = vVel; 
    }
}
