/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class AD_PC : MonoBehaviour
{
    public AudioSource                              PF_Gunshot;
    public AudioSource                              PF_MisFire;

    public AudioSource                              _adShieldsPanicking;
    public AudioSource                              _adShieldsRecharging;

    void Start()
    {
        _adShieldsPanicking.Stop();
        _adShieldsRecharging.Stop();
    }

    public void FFireGun()
    {
        Instantiate(PF_Gunshot, transform.position, transform.rotation);
    }
    public void FMisFireGun()
    {
        Instantiate(PF_MisFire, transform.position, transform.rotation);        
    }

    public void FPlayShieldsRecharging(float curVal, float maxVal)
    {
        _adShieldsRecharging.Play();
    }
    public void FStopPlayShieldsRecharging()
    {
        _adShieldsRecharging.Stop();
    }

    public void FPlayShieldsPanicking()
    {
        _adShieldsPanicking.Play();
    }
    public void FStopPlayShieldsPanicking()
    {
        _adShieldsPanicking.Stop();
    }
}
