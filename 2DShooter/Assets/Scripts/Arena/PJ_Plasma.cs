﻿/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PJ_Plasma : PJ_Base
{

    void Update()
    {
        if(cLifetime._lifeOver){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<EN_Melee>())
        {
            Destroy(gameObject);
        }
    }
}
