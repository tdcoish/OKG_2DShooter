/************************************************************
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_Node : MonoBehaviour
{
    public List<AI_Node>                        rConNodes;

    // The node that we need to go through to get to us.
    [HideInInspector]
    public int                          _ixPrevNode;

    // The distance to this node from our original node. Used for A*.
    [HideInInspector]
    public float                        _disToStart; 

    // The distance directly to the end node. Needed for A*
    [HideInInspector]
    public float                        _disToGoal;

    void OnDrawGizmos ()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach(var node in rConNodes){
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
        
    }
}
