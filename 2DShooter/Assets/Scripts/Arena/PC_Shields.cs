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
        }
    }

    private void ENTER_Charged(){
        _state = STATE.S_CHARGED;
    }
    private void ENTER_Charging(){
        _state = STATE.S_CHARGING;
    }
    private void RUN_Charging()
    {
        _val += _rechargeRate * Time.deltaTime;
        if(_val > _maxVal){
            _val = _maxVal;
            ENTER_Charged();
        }
        cAudio.FPlayShieldsRecharging(_val, _maxVal);
    }
    private void EXIT_Charging(){
        cAudio.FStopPlayShieldsRecharging();
    }

    private void ENTER_Panicking(){
        _state = STATE.S_PANICKING;
        _lastDamTakenTime = Time.time;
    }
    private void RUN_Panicking()
    {
        if(Time.time - _lastDamTakenTime > _timeBeforeRechargeStarts)
        {
            EXIT_Panicking();
            ENTER_Charging();
        }
    }
    private void EXIT_Panicking(){
        cAudio.FStopPlayShieldsPanicking();
    }

    public float FTakeDamageGiveRemainder(float dam)
    {
        ENTER_Panicking();
        _lastDamTakenTime = Time.time;
        _val -= dam;

        float damLeft;
        if(_val >= 0f){
            damLeft = 0f;
        }else{
            damLeft = _val * -1f;
            _val = 0f;
        }

        return damLeft;
    }
}
