﻿/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Plasma : MonoBehaviour
{
    private Rigidbody2D                             cRigid;
    public float                                    _spd;

    void Awake()
    {
        cRigid = GetComponent<Rigidbody2D>();    
        if(cRigid == null){
            Debug.Log("NO RIGIDBODY");
        }
    }

    public void FFireDirection(Vector3 vDir)
    {
        vDir = Vector3.Normalize(vDir);
        vDir *= _spd;
        cRigid.velocity = vDir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EN_Melee>())
        {
            Destroy(gameObject);
        }
    }
}
