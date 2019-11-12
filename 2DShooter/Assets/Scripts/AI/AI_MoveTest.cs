/************************************************************
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_MoveTest : MonoBehaviour
{
    public float                                        _spd = 5f;
    private AI_Pathfind                                 cPather;
    private Rigidbody2D                                 cRigid;

    public PC_Cont                                      rPC;

    void Start()
    {
        cPather = GetComponent<AI_Pathfind>();    
        cRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        List<AI_Node> path = cPather.FFindPath(transform.position, rPC.transform.position);

        Vector3 vDir = path[0].transform.position - transform.position;
        cRigid.velocity = vDir.normalized * _spd;
    }
}
