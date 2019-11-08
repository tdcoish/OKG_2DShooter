/*************************************************************************************
For now it's just like Halo style.
*************************************************************************************/
using UnityEngine;

public class PC_Shields : MonoBehaviour
{
    public enum STATE{
        S_CHARGING,
        S_PANICKING
    }
    public STATE                            _state;

    public float                            _maxVal = 100f;
    public float                            _val;
    private float                           _lastDamTakenTime;
    public float                            _timeBeforeRechargeStarts = 2f;
    public float                            _rechargeRate = 20f;

    void Start()
    {
        _state = STATE.S_CHARGING;
    }

    void Update()
    {
        switch(_state)
        {
            case STATE.S_CHARGING: RUN_Charging(); break;
            case STATE.S_PANICKING: RUN_Panicking(); break;
        }
    }

    private void ENTER_Charging(){
        _state = STATE.S_CHARGING;
    }
    private void RUN_Charging()
    {
        _val += _rechargeRate * Time.deltaTime;
        if(_val > _maxVal){
            _val = _maxVal;
        }
    }

    private void ENTER_Panicking(){
        _state = STATE.S_PANICKING;
        _lastDamTakenTime = Time.time;
    }
    private void RUN_Panicking()
    {
        if(Time.time - _lastDamTakenTime > _timeBeforeRechargeStarts)
        {
            ENTER_Charging();
        }
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
