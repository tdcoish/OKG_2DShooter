/*************************************************************************************
Spawn in the different enemies.
*************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Man : MonoBehaviour
{
    [SerializeField]
    private EN_Melee                PF_Melee;
    public float                    _spawnRate;
    private float                   _lastSpawn;
    public GM_Spawn[]               _spawnPoints;

    void Start()
    {
        TDC_EventManager.FAddHandler(TDC_GE.GE_PCDeath, E_PlayerDied);
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

    private void E_PlayerDied()
    {
        SceneManager.LoadScene("Death");        
    }
}
