﻿/************************************************************
Manages spawns, listens for enemies dying, etcetera.
************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVL_Man : MonoBehaviour
{

    public enum STATE{
        S_Spawning,
        S_DoneSpawning
    }
    public STATE                                    _state;

    public LVL_Spawner[]                            rSpawners;
    public EN_Melee                                 PF_Melee;

    private int                                     _numSpawned;
    private int                                     _numKilled;

    private float                                   _lastSpawnTime;
    public float                                    _spawnInterval;
    public int                                      _numWaves;
    private int                                     _ixWave;

    void Awake()
    {
        TDC_EventManager.FRemoveAllHandlers();

        DT_Wave w = new DT_Wave(1, 1, 4);
        IO_Wave.FSaveWave(w);
        // w = IO_Wave.FLoadWave(1.ToString());
        for(int i=2; i<5; i++){
            w._id = i;
            w._numWaves = i;
            IO_Wave.FSaveWave(w);
        }
    }

    void Start()
    {
        TDC_EventManager.FAddHandler(TDC_GE.GE_EDeath, E_EnemyDied);

        DT_Wave w = IO_Wave.FLoadWave(IO_Prog._curLevel.ToString());
        IO_Prog._numLevels = 2;
        _numWaves = w._numWaves;

        _state = STATE.S_Spawning;
        rSpawners = FindObjectsOfType<LVL_Spawner>();
        _lastSpawnTime = _spawnInterval * -1;
    }

    void Update()
    {
        if(_ixWave >= _numWaves)
        {
            _state = STATE.S_DoneSpawning;
        }
        else{
            if(Time.time - _lastSpawnTime > _spawnInterval)
            {
                for(int i=0; i<rSpawners.Length; i++)
                {
                    _numSpawned++;
                    Instantiate(PF_Melee, rSpawners[i].transform.position, rSpawners[i].transform.rotation);
                }
                _lastSpawnTime = Time.time;
                _ixWave++;
            }
        }

        if(_state == STATE.S_DoneSpawning)
        {
            if(_numKilled == _numSpawned){
                Debug.Log("You win");
                IO_Prog._curLevel++;
                if(IO_Prog._curLevel > IO_Prog._numLevels){
                    SceneManager.LoadScene("Won");
                }else{
                    SceneManager.LoadScene("WaveTextTest");
                }
            }
        }
    }

    void E_EnemyDied()
    {
        _numKilled++;
    }
}
