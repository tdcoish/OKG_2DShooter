/************************************************************
Script that manages the winning screen.
************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class WS_Man : MonoBehaviour
{
    public enum STATE
    {
        S_Countdown,
        S_Chill
    }
    private STATE                           _state;

    public float                            _countDownTime;
    public float                            _startTime;

    public WS_UI                            rUI;

    void Start()
    {
        _state = STATE.S_Countdown;
        _startTime = Time.time;
    }

    void Update()
    {

        switch(_state)
        {
            case STATE.S_Countdown: RUN_Countdown(); break;
            case STATE.S_Chill: RUN_Chill(); break;
        }
    }

    private void RUN_Countdown()
    {
        if(Time.time - _startTime > _countDownTime)
        {
            rUI.FShowInstr();
            _state = STATE.S_Chill;
        }
    }
    private void RUN_Chill()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
