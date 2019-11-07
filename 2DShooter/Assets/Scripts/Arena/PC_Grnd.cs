/*************************************************************************************
Grenade launcher.
*************************************************************************************/
using UnityEngine;

public class PC_Grnd : MonoBehaviour
{

    public float                                _fireRate;
    private float                               _lastFire;
    public PJ_Grenade                           PF_Grenade;
    
    void Start()
    {
        _lastFire = _fireRate * -1f;
    }

    public void FRun()
    {
        if(Input.GetMouseButton(1)){
            if(Time.time - _lastFire > _fireRate)
            {
                PJ_Grenade p = Instantiate(PF_Grenade, transform.position, transform.rotation);
                Vector3 vDir = FindObjectOfType<UI_CrossHair>().transform.position - transform.position;
                p.FFireDirection(vDir);
                _lastFire = Time.time;
            }
        }
    }
}
