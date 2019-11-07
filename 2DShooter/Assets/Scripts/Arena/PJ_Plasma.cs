﻿/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Plasma : MonoBehaviour
{
    private Rigidbody2D                             cRigid;
    public float                                    _spd;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();    
    }

    public void FFireDirection(Vector3 vDir)
    {
        vDir = Vector3.Normalize(vDir);
        vDir *= _spd;
        cRigid.velocity = vDir;
    }

    private void OnColliderEnter(Collider other)
    {
        Debug.Log("Hit something");
        if(other.GetComponent<EN_Melee>())
        {
            Debug.Log("Hit the enemy melee");
            Destroy(gameObject);
        }
    }
}
