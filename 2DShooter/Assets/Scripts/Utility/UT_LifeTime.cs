/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class UT_LifeTime : MonoBehaviour
{
    public bool                             _lifeOver = false;
    public float                            _lifespan = 1f;
    private float                           _spawnTime;

    void Start()
    {
        _spawnTime = Time.time;
    }

    void Update()
    {
        if(!_lifeOver){
            if(Time.time - _spawnTime > _lifespan){
                _lifeOver = true;
            }
        }
    }
}
