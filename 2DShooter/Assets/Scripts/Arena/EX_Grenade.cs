/*************************************************************************************
The grenade spawns this entity.
*************************************************************************************/
using UnityEngine;

[RequireComponent(typeof(UT_LifeTime))]
public class EX_Grenade : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<UT_LifeTime>()._lifeOver){
            Destroy(gameObject);
        }
    }
}
