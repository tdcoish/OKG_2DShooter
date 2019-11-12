/************************************************************
Alrighty. What we're going to need is a list of all the AI_Node
entities, as well as all the 
************************************************************/
using UnityEngine;

public class AI_Pathfind : MonoBehaviour
{
    private AI_Node[]                       rNodes;
    void Start()
    {
        rNodes = FindObjectsOfType<AI_Node>();     
    }

    void Update()
    {
        
    }
}
