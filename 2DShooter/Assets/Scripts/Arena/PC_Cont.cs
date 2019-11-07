/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PC_Cont : MonoBehaviour
{
    private Rigidbody2D                     cRigid;
    private PC_Gun                          cGun;
    
    public float                            _spd;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>(); 
        cGun = GetComponent<PC_Gun>();   
    }

    void Update()
    {
        cRigid.velocity = HandleInputForVel();

    }

    private Vector3 HandleInputForVel()
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
        return vVel;
    }

}
