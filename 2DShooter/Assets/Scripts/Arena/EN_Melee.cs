/*************************************************************************************
Zerg rush the player.
*************************************************************************************/
using UnityEngine;

public class EN_Melee : MonoBehaviour
{
    private Rigidbody2D                 cRigid;

    public float                        _spd;
    PC_Cont                             rPC;

    void Start()
    {
        cRigid = GetComponent<Rigidbody2D>();
        rPC = FindObjectOfType<PC_Cont>();
    }

    void Update()
    {
        Vector2 vDif = rPC.transform.position - transform.position;
        vDif = Vector3.Normalize(vDif);
        cRigid.velocity = vDif * _spd;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PJ_Plasma>() != null){
            Destroy(gameObject);
        }
    }
}
