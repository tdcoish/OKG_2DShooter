/************************************************************
The text for the title cards/in between waves.
************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_SplashText : MonoBehaviour
{
    public enum STATE
    {
        S_INTRO,
        S_CHILL,
        S_OUTRO
    }
    public STATE                            _state;
    public Text                             _txt;

    private float                           _stateChangeTime;

    public float                            _introTime = 0.5f;
    public float                            _chillTime = 5f;
    public float                            _outroTime = 1f;

    void Start()
    {
        ENTER_Intro();
    }

    void Update()
    {
        switch(_state)
        {
            case STATE.S_INTRO: RUN_Intro(); break;
            case STATE.S_CHILL: RUN_Chill(); break;
            case STATE.S_OUTRO: RUN_Outro(); break;
        }
    }

    private void ENTER_Intro()
    {
        _state = STATE.S_INTRO;
        _stateChangeTime = Time.time;
        Color c = _txt.color;
        c.a = 0f;
        _txt.color = c;
    }
    private void RUN_Intro()
    {
        // make the alpha steadily increase.
        float timeSinceStart = Time.time - _stateChangeTime;
        float perc = timeSinceStart / _introTime;
        Color c = _txt.color;
        c.a = perc;
        _txt.color = c;

        if(perc > 1f)
        {
            ENTER_Chill();
        }
    }
    private void ENTER_Chill()
    {
        _state = STATE.S_CHILL;
        _stateChangeTime = Time.time;
    }
    private void RUN_Chill()
    {
        if(Time.time - _stateChangeTime >= _chillTime)
        {
            ENTER_Outro();
        }
    }
    private void ENTER_Outro()
    {
        _state = STATE.S_OUTRO;
        _stateChangeTime = Time.time;
    }
    private void RUN_Outro()
    {
        float timeSinceStart = Time.time - _stateChangeTime;
        float perc = timeSinceStart / _outroTime;
        perc = 1f - perc;
        Color c = _txt.color;
        c.a = perc;
        _txt.color = c;

        if(perc <= 0f){
            Debug.Log("Should be finished");
            SceneManager.LoadScene("WaveSystemTest");
        }
    }
}
