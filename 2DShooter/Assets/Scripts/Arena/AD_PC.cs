/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class AD_PC : MonoBehaviour
{
    public AudioSource                              PF_Gunshot;
    public AudioSource                              PF_MisFire;

    public void FFireGun()
    {
        Instantiate(PF_Gunshot, transform.position, transform.rotation);
    }
    public void FMisFireGun()
    {
        Instantiate(PF_Gunshot, transform.position, transform.rotation);        
    }
}
