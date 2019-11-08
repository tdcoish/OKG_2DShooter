/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class AD_PC : MonoBehaviour
{
    public AudioSource                              PF_Gunshot;

    public void FFireGun()
    {
        Instantiate(PF_Gunshot, transform.position, transform.rotation);
    }
}
