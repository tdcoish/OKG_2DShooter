/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class PC_Gun : MonoBehaviour
{

    public float                            _fireRate;
    private float                           _lastFire;
    public PJ_Plasma                        PF_Plasmoid;

    public void FRun()
    {
        if(Input.GetMouseButton(0)){
            if(Time.time - _lastFire > _fireRate)
            {
                PJ_Plasma p = Instantiate(PF_Plasmoid, transform.position, transform.rotation);
                p.FFireDirection(FindObjectOfType<PC_Cont>().transform.position - transform.position);
            }
        }
    }

}
