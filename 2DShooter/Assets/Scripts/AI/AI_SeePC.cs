/************************************************************
Returns true if you can see the player. False if not.
************************************************************/
using UnityEngine;

public class AI_SeePC : MonoBehaviour
{
    public bool FCanSeePlayer(Vector3 pcPos)
    {
        Vector3 vGoal = pcPos;
        Vector3 vDir = vGoal - transform.position;
        float dis = Vector3.Distance(transform.position, pcPos);
        Debug.DrawLine(transform.position, transform.position + vDir*dis, Color.cyan);
        LayerMask mask = LayerMask.GetMask("PC", "Level Geometry", "Obstacles");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vDir, dis*1.1f, mask);
        if(hit.collider != null){
            if(hit.collider.GetComponent<PC_Cont>() == null)
            {
                return false;
            }
            // else{
            //     Debug.Log(hit.collider);
            // }
        }

        return true;
    }
}
