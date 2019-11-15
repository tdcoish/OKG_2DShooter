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
        
    }

    void Update()
    {
        
    }
}
