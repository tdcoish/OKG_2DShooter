/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PC_Gun : MonoBehaviour
{

    public float                            _fireRate;
    private float                           _lastFire;
    public PJ_Plasma                        PF_Plasmoid;

    private AD_PC                           cAudio;

    void Start()
    {
        cAudio = GetComponentInChildren<AD_PC>();
    }

    public void FRun()
    {
        if(Input.GetMouseButton(0)){
            if(Time.time - _lastFire > _fireRate)
            {
                PJ_Plasma p = Instantiate(PF_Plasmoid, transform.position, transform.rotation);
                Vector3 vDir = FindObjectOfType<UI_CrossHair>().transform.position - transform.position;
                p.FFireDirection(vDir);
                cAudio.FFireGun();
                _lastFire = Time.time;
            }
        }
    }

}
