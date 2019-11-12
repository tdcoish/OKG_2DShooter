/************************************************************
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_MoveTest : MonoBehaviour
{
    public float                                        _spd = 5f;
    private AI_Pathfind                                 cPather;
    private Rigidbody2D                                 cRigid;

    List<AI_Node>                                       _pathList;

    public PC_Cont                                      rPC;

    void Start()
    {
        cPather = GetComponent<AI_Pathfind>();    
        cRigid = GetComponent<Rigidbody2D>();

        _pathList = cPather.FFindPath(transform.position, rPC.transform.position);
    }

    void Update()
    {
        // if(_pathList == null){
        //     Debug.Log("No path");
        //     return;
        // }else{
        //     for(int i=1; i<_pathList.Count; i++){
        //         Debug.DrawLine(_pathList[i].transform.position, _pathList[i-1].transform.position);
        //     }
        // }
        // Vector3 vDir = _pathList[0].transform.position - transform.position;
        // cRigid.velocity = vDir.normalized * _spd;
    }
}
