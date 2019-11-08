/*************************************************************************************
Different from UT_LifeTime, this one just straight up deletes the entity after a set amount
of time. Use this for little things that you don't want cluttering up the game world.
*************************************************************************************/
using UnityEngine;

public class UT_KillTime : MonoBehaviour
{
    public float                            _lifeTime = 10f;
    private float                           _startTime;

    void Start()
    {
        _startTime = Time.time;    
    }

    void Update()
    {
        if(Time.time - _startTime > _lifeTime){
            Destroy(gameObject);
        }
    }
}
