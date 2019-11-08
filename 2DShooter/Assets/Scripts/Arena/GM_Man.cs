/*************************************************************************************
Spawn in the different enemies.
*************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Man : MonoBehaviour
{
    [SerializeField]
    private EN_Melee                PF_Melee;
    [SerializeField]
    private PCK_Health              PF_HealthPack;

    public float                    _spawnRate;
    private float                   _lastSpawn;
    public GM_Spawn[]               _spawnPoints;

    public float                    _healthSpawnRate;
    public float                    _lastHealthSpawn;
    public GM_HP_Spawn[]            _healthSpawnPoints;

    void Start()
    {
        TDC_EventManager.FAddHandler(TDC_GE.GE_PCDeath, E_PlayerDied);

        _lastSpawn = _spawnRate * -1f;
    }

    void Update()
    {
        if(Time.time - _lastSpawn > _spawnRate){
            for(int i=0; i<_spawnPoints.Length; i++){
                Instantiate(PF_Melee, _spawnPoints[i].transform.position, transform.rotation);
                _lastSpawn = Time.time;
            }
        }

        if(Time.time - _lastHealthSpawn > _healthSpawnRate){
            for(int i=0; i<_healthSpawnPoints.Length; i++){
                Instantiate(PF_HealthPack, _healthSpawnPoints[i].transform.position, transform.rotation);
                _lastHealthSpawn = Time.time;
            }
        }  

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }  
    }

    private void E_PlayerDied()
    {
        SceneManager.LoadScene("Death");        
    }
}
