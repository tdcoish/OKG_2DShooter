/************************************************************
Alrighty. What we're going to need is a list of all the AI_Node
entities, as well as all the 
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_Pathfind : MonoBehaviour
{
    private AI_Node[]                       rNodes;
    void Start()
    {
        rNodes = FindObjectsOfType<AI_Node>();     
    }

    /******************************************************************************
    Is given the current position of the thingy, as well as the 
    **************************************************************************** */
    public List<AI_Node> FFindPath(Vector3 vCurPos, Vector3 vDestPos)
    {
        // ------------------ Store the distance to the goal for all nodes.
        for(int i=0; i<rNodes.Length; i++)
        {
            rNodes[i]._disToGoal = Vector3.Distance(rNodes[i].transform.position, vDestPos);
        }

        // ------------------ Select the starting nodes from some candidates.
        List<AI_Node> rVisible = new List<AI_Node>();

        for(int i=0; i<rNodes.Length; i++){
            Vector2 vDir = rNodes[i].transform.position - vCurPos;
            RaycastHit2D hit = Physics2D.Raycast(vCurPos, vDir);
            if(hit.collider != null){
                if(hit.collider.gameObject == rNodes[i].gameObject)
                {
                    Debug.Log("Yeah we can see the node: " + i);
                    Debug.DrawLine(vCurPos, rNodes[i].transform.position);
                    Debug.DrawLine(rNodes[i].transform.position, vDestPos, Color.green);    
                    rVisible.Add(rNodes[i]);
                }
            }
        }        
        if(rVisible.Count == 0){
            Debug.Log("ERROR. No nodes visible.");
            return null;
        }

        // Technically I should be doing distance calculations to everything in here.
        // figure out which one is closest to the goal.
        // But fuck it, just pick the one that's closest to the goal
        int ixStart = 0;
        float dis = rVisible[ixStart]._disToGoal;
        Debug.Log("Distance: " + dis);
        Debug.Log("Num Visible: " + rVisible.Count);
        for(int i=1; i<rVisible.Count; i++){
            if(rVisible[i]._disToGoal < dis){
                Debug.Log("Changing");
                dis = rVisible[i]._disToGoal;
                ixStart = i;
                Debug.Log("Distance: " + dis);
            }
            Debug.Log("Dist: " + rVisible[i]._disToGoal);
        }

        // Now that we have our starting node, let's just return that and see if that's working.
        List<AI_Node> path = new List<AI_Node>();
        path.Add(rVisible[ixStart]);
        return path;
    }
}
