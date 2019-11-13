/************************************************************
Movement needs two different systems. The first, we figure out if we 
can just directly go to where we need to go. If we can do that, then
we just go there.

The second, if we need to pathfind, then we figure out the path, and follow
that until we do not need to any longer.
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
        // Find out which form of pathing we need to follow. If we can just straight up move to the target location, then
        // just move there. If not, pathfind.
        bool usePathing = true;

        Vector3 vGoal = rPC.transform.position;
        Vector2 vDir = vGoal - transform.position;
        float dis = Vector3.Distance(transform.position, rPC.transform.position);
        Debug.DrawLine(transform.position, (transform.position + (Vector3)vDir*dis), Color.cyan);
        LayerMask mask = LayerMask.GetMask("PC", "Level Geometry");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vDir, dis*1.1f, mask);
        if(hit.collider != null){
            if(hit.collider.GetComponent<PC_Cont>())
            {
                usePathing = false;
            }else{
                Debug.Log(hit.collider);
            }
        }

        if(usePathing)
        {
            // here we get a new path.
            _pathList = cPather.FFindPath(transform.position, rPC.transform.position);
            if(_pathList == null){
                Debug.Log("No valid path");
                return;
            }
            if(_pathList.Count == 0 || _pathList == null){
                Debug.Log("No nodes on that path");
                return;
            }
            if(Vector3.Distance(_pathList[0].transform.position, transform.position) < 2f){
                _pathList.RemoveAt(0);
            }
            if(_pathList.Count == 0 || _pathList == null){
                Debug.Log("No valid path");
                return;
            }
            vDir = _pathList[0].transform.position - transform.position;
            cRigid.velocity = Vector3.Normalize(vDir) * _spd;

            for(int i=1; i<_pathList.Count; i++){
                Debug.DrawLine(_pathList[i].transform.position, _pathList[i-1].transform.position);
            }
        }else{
            vDir = rPC.transform.position - transform.position;
            cRigid.velocity = Vector3.Normalize(vDir) * _spd;            
        }

    }
}
