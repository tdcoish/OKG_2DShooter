/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class AD_PC : MonoBehaviour
{
    public AudioSource                              _Gunshot;

    public void FFireGun()
    {
        _Gunshot.Play();
    }
}
