/************************************************************
************************************************************/
using UnityEngine;

public class AD_Sniper : MonoBehaviour
{
    public AudioSource                              _shot;

    public void FPlayFire()
    {
        _shot.Play();
    }
}
