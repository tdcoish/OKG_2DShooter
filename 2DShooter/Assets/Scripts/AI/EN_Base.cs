﻿/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AI_Pathfind))]
[RequireComponent(typeof(AI_SeePC))]
public class EN_Base : MonoBehaviour
{
    protected Rigidbody2D               cRigid;
    protected AI_Pathfind               cPath;
    protected AI_SeePC                  cSeePC;

    protected List<AI_Node>             _pathList;


    public float                        _spd;
    protected PC_Cont                   rPC;

    public float                        _health = 100f;

    public ParticleSystem               PF_HitByBullet;
    public ParticleSystem               PF_Explode;
    public GFX_Gibs                     PF_Gibs;

    public float                        _dropAmmoChance = 10f;
    public float                        _dropHealthChance = 10f;
    public PCK_Ammo                     PF_AmmoBox;
    public PCK_Health                   PF_HealthBox;

    public float                        _visionDistance = 20f;
 
 
    protected void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
        cPath = GetComponent<AI_Pathfind>();
        cSeePC = GetComponent<AI_SeePC>();
        rPC = FindObjectOfType<PC_Cont>();
    }

    protected virtual void KillYourself()
    {
        Instantiate(PF_Explode, transform.position, transform.rotation);
        Instantiate(PF_Gibs, transform.position, transform.rotation);

        // Randomly decide to spawn in ammo or health.
        {
            float random = Random.Range(0, 100f);
            if(random < _dropAmmoChance){
                Instantiate(PF_AmmoBox, transform.position, transform.rotation);
            }else{
                random = Random.Range(0, 100f);
                if(random < _dropHealthChance){
                    Instantiate(PF_HealthBox, transform.position, transform.rotation);
                }
            }
        }

        TDC_EventManager.FBroadcast(TDC_GE.GE_EDeath);

        Destroy(gameObject);
    }
}
