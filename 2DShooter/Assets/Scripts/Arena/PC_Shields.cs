/*************************************************************************************
For now it's just like Halo style.

Might as well do the audio for this as well.
*************************************************************************************/
using UnityEngine;

public class PC_Shields : MonoBehaviour
{
    public enum STATE{
        S_CHARGED,
        S_CHARGING,
        S_DELAYED,          // this is where they still have shields, but they just got hit and aren't charging.
        S_PANICKING
    }
    public STATE                            _state;

    private AD_PC                           cAudio;

    public float                            _maxVal = 100f;
    public float                            _val;
    private float                           _lastDamTakenTime;
    public float                            _timeBeforeRechargeStarts = 2f;
    public float                            _rechargeRate = 20f;

    void Start()
    {
        cAudio = GetComponentInChildren<AD_PC>();
        _val = _maxVal;
        ENTER_Charged();
    }

    void Update()
    {
        switch(_state)
        {
            case STATE.S_CHARGING: RUN_Charging(); break;
            case STATE.S_PANICKING: RUN_Panicking(); break;
            case STATE.S_CHARGED: RUN_Charged(); break;
            case STATE.S_DELAYED: RUN_Delayed(); break;
        }
    }

    private void ENTER_Charged(){
        _state = STATE.S_CHARGED;
        cAudio.FStopPlayShieldsRecharging();
    }
    private void RUN_Charged(){}
    private void ENTER_Charging(){
        _state = STATE.S_CHARGING;
    }
    private void RUN_Charging()
    {
        cAudio.FPlayShieldsRecharging(_val, _maxVal);
        _val += _rechargeRate * Time.deltaTime;
        if(_val > _maxVal){
            _val = _maxVal;
            ENTER_Charged();
        }
    }
    private void EXIT_Charging(){
        cAudio.FStopPlayShieldsRecharging();
    }

    private void ENTER_Panicking(){
        _state = STATE.S_PANICKING;
        _lastDamTakenTime = Time.time;
        cAudio.FPlayShieldsPanicking();
    }
    private void RUN_Panicking()
    {
        if(Time.time - _lastDamTakenTime > _timeBeforeRechargeStarts)
        {
            EXIT_Panicking();
            ENTER_Charging();
        }
    }
    private void ENTER_Delayed(){
        _state = STATE.S_DELAYED;
        _lastDamTakenTime = Time.time;
    }
    private void RUN_Delayed()
    {
        if(Time.time - _lastDamTakenTime > _timeBeforeRechargeStarts)
        {
            ENTER_Charging();
        }
    }
    private void EXIT_Panicking(){
        cAudio.FStopPlayShieldsPanicking();
    }

    public float FTakeDamageGiveRemainder(float dam)
    {
        _lastDamTakenTime = Time.time;
        _val -= dam;

        float damLeft;
        if(_val >= 0f){
            damLeft = 0f;
            ENTER_Delayed();
        }else{
            damLeft = _val * -1f;
            _val = 0f;
            ENTER_Panicking();
        }

        return damLeft;
    }
}
