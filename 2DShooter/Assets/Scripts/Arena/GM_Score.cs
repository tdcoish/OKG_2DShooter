/*************************************************************************************
Score manager.
*************************************************************************************/
using UnityEngine;

public class GM_Score : MonoBehaviour
{
    public UI_PC                            rUI;

    public int                              _score;

    public int                              _enemyDeath;
    public int                              _secondSurvived;
    public int                              _ammoPickedUp;
    public int                              _healthPickedUp;

    private int                             _lastSec;

    public void E_AmmoPickedUp()
    {
        _score += _ammoPickedUp;
        rUI.FSetScoreText(_score);
    }

    public void E_HealthPickedUp()
    {
        _score += _healthPickedUp;
        rUI.FSetScoreText(_score);
    }

    public void E_EnemyDies()
    {
        _score += _enemyDeath;
        rUI.FSetScoreText(_score);
    }

    private void E_Sec()
    {
        _score += _secondSurvived;
        rUI.FSetScoreText(_score);
    }

    void Start()
    {
        TDC_EventManager.FAddHandler(TDC_GE.GE_EDeath, E_EnemyDies);
        TDC_EventManager.FAddHandler(TDC_GE.GE_PCK_HLT, E_HealthPickedUp);
        TDC_EventManager.FAddHandler(TDC_GE.GE_PCK_AM, E_AmmoPickedUp);

        _lastSec = 0;
        rUI.FSetScoreText(_score);
    }

    void Update()
    {
        int sec = (int)Time.time % 10;
        if(_lastSec != sec){
            _lastSec = sec;
            E_Sec();
        }
    }

}
