/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PC_Cont : MonoBehaviour
{
    private Rigidbody2D                     cRigid;
    
    public float                            _spd;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        Vector2 vVel = new Vector2();
        if(Input.GetKey(KeyCode.A)){
            vVel.x -= _spd;
        }
        if(Input.GetKey(KeyCode.D)){
            vVel.x += _spd;
        }
        if(Input.GetKey(KeyCode.W)){
            vVel.y += _spd;
        }
        if(Input.GetKey(KeyCode.S)){
            vVel.y -= _spd;
        }

        cRigid.velocity = vVel;
    }
}
