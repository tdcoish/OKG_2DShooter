/************************************************************
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_Node : MonoBehaviour
{
    public List<AI_Node>                        rConNodes;

    void OnDrawGizmos ()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach(var node in rConNodes){
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
        
    }
}
