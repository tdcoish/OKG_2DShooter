/*************************************************************************************
Spawn in the different enemies.
*************************************************************************************/
using UnityEngine;

public class GM_Man : MonoBehaviour
{
    [SerializeField]
    private EN_Melee                PF_Melee;
    public float                    _spawnRate;
    private float                   _lastSpawn;
    public GM_Spawn[]               _spawnPoints;

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time - _lastSpawn > _spawnRate){
            for(int i=0; i<_spawnPoints.Length; i++){
                Instantiate(PF_Melee, _spawnPoints[i].transform.position, transform.rotation);
                _lastSpawn = Time.time;
            }
        }
    }
}
