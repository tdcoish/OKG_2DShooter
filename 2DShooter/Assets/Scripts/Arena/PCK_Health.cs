﻿/*************************************************************************************

*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(UT_LifeTime))]
public class PCK_Health : MonoBehaviour
{
    public ParticleSystem                       PF_Particles;

    void Update()
    {
        if(GetComponent<UT_LifeTime>()._lifeOver){
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
