/************************************************************
Manages spawns, listens for enemies dying, etcetera.
************************************************************/
using UnityEngine;

public class LVL_Man : MonoBehaviour
{
    public LVL_Spawner[]                            rSpawners;
    public EN_Melee                                 PF_Melee;

    private float                                   _lastSpawnTime;
    public float                                    _spawnInterval;
    public int                                      _numWaves;
    private int                                     _ixWave;

    void Start()
    {
        _lastSpawnTime = _spawnInterval * -1;
    }

    void Update()
    {
        if(_ixWave >= _numWaves)
        {
            return;
        }
        if(Time.time - _lastSpawnTime > _spawnInterval)
        {
            for(int i=0; i<rSpawners.Length; i++)
            {
                Instantiate(PF_Melee, rSpawners[i].transform.position, rSpawners[i].transform.rotation);
            }
            _lastSpawnTime = Time.time;
            _ixWave++;
        }
    }
}
