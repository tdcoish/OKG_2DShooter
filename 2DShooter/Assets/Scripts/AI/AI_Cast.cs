/************************************************************
Cast out and see if we can see the player.
************************************************************/
using UnityEngine;

public class AI_Cast : MonoBehaviour
{
    public PC_Cont                              rPC;

    void Update()
    {
        Vector2 vDir = rPC.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vDir);

        if(hit.collider != null)
        {
            if(hit.collider.GetComponent<PC_Cont>())
            {
                Debug.Log("We hit the player");
            }else if(hit.collider.GetComponent<ENV_Wall>()){
                Debug.Log("Wall in way");
            }else{
                Debug.Log("Something other than wall or player");
            }
        }else{
            Debug.Log("Hit nothing in the scene");
        }
    }
}
