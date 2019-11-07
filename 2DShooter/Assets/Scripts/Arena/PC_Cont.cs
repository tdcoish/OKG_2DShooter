﻿/*************************************************************************************

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
        RotateToMouse();
        cGun.FRun();
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

    private void RotateToMouse(){
		Camera c = Camera.main;
		Vector2 msPos = c.ScreenToWorldPoint(Input.mousePosition);

		Vector2 distance = msPos - (Vector2)transform.position;
		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		angle -= 90;
		transform.eulerAngles = new Vector3(0, 0, angle);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit something");
        if(other.GetComponent<EN_Melee>()){
            Debug.Log("Her");
            TDC_EventManager.FBroadcast(TDC_GE.GE_PCDeath);
        }
    }

}
