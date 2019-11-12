/************************************************************
Alrighty. What we're going to need is a list of all the AI_Node
entities, as well as all the 

Alright. Ultimately, we are going to need to raycast to all the 
potential nodes, and then see which path is the shortest.

It's an extra step of complexity, but it gives better results than
the other way.
************************************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI_Pathfind : MonoBehaviour
{
    private AI_Node[]                       rNodes;
    void Awake()
    {
        rNodes = FindObjectsOfType<AI_Node>();     
    }

    /******************************************************************************
    Is given the current position of the thingy, as well as the 
    **************************************************************************** */
    public List<AI_Node> FFindPath(Vector3 vCurPos, Vector3 vDestPos)
    {
        // ----------------- Make a list of visible nodes
        List<AI_Node> rVisible = new List<AI_Node>();
        for(int i=0; i<rNodes.Length; i++){
            Vector2 vDir = rNodes[i].transform.position - vCurPos;
            RaycastHit2D hit = Physics2D.Raycast(vCurPos, vDir);
            if(hit.collider != null){
                if(hit.collider.gameObject == rNodes[i].gameObject)
                {
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
        
        // ------------------ Figure out which node is going to be the goal.
        // For now just use closest. That's not always perfect, but it's good enough.
        int ixGoal = 0;
        float dis = Vector3.Distance(rNodes[0].transform.position, vDestPos);
        for(int i=1; i<rNodes.Length; i++)
        {
            float fDis = Vector3.Distance(rNodes[i].transform.position, vDestPos);
            if(fDis < dis){
                dis = fDis;
                ixGoal = i;
            }
        }

        // ------------------ Store the distance to the goal for all nodes.
        for(int i=0; i<rNodes.Length; i++)
        {
            rNodes[i]._disToGoal = Vector3.Distance(rNodes[i].transform.position, rNodes[ixGoal].transform.position);
        }
        

        // ----------------- For each visible node, calculate a path starting from there.
        List<AI_Node> path = new List<AI_Node>();
        List<AI_Node> temp = new List<AI_Node>();
        dis = 100000f;
        for(int i=0; i<rVisible.Count; i++)
        {
            for(int j=0; j<rNodes.Length; j++)
            {
                if(rNodes[j] == rVisible[i])
                {
                    temp = CalcPathFromNodes(vCurPos, j, ixGoal);
                    if(temp == null){
                        Debug.Log("Null list");
                        continue;
                    }
                    if(temp[temp.Count-1]._disToStart < dis){
                        path = temp;
                        dis = temp[temp.Count-1]._disToStart;
                    }
                }
            }
        }

        return path;
    }

    private List<AI_Node> CalcPathFromNodes(Vector3 vCurPos, int ixStart, int ixGoal)
    {
                // ---------------- Run A* and calculate the best path.
        // 1. Assign to every node a tentative distance of infinity, or some huge number, ie. 10000
        for(int i=0; i<rNodes.Length; i++){
            rNodes[i]._disToStart = 100000f;
        }
        // 2. Set the distance of the starting node to 0
        rNodes[ixStart]._disToStart = 0f;

        // 3. Keep a set of visited node indices. Start with just the initial node.
        bool[] visited = new bool[rNodes.Length];
        for(int i=0; i<visited.Length; i++) visited[i] = false;

        // 4. Just a debugging thing, if we hit a -1 node, there is no correct path.
        for(int i=0; i<rNodes.Length; i++) rNodes[i]._ixPrevNode = -1;

        // 6. Try to find a path. As soon as we find one, return that.
        bool foundPath = false;
        int ixCur = -1;

        Debug.DrawLine(vCurPos, rNodes[ixStart].transform.position, Color.magenta);
        while(!foundPath)
        {
            // 7. Get the unvisited node with the lowest tentative distance. Make this the current node to work with.
            ixCur = -1;
            ixCur = FindSmallestUnvisitedNode(visited, rNodes);

            // 8. Now that we have the correct node, visit all its neighbours, update their distances if appropriate.
            for(int i=0; i<rNodes[ixCur].rConNodes.Count; ++i)
            {
                // Get the distance of the path from our current node, to the next connecting node.
                float dis = Vector3.Distance(rNodes[ixCur].transform.position, rNodes[ixCur].rConNodes[i].transform.position);
                dis += rNodes[ixCur]._disToStart;

                // Check that the distance we're about to put in is actually lower than the distance already in there. If so, put it in.
                if(dis < rNodes[ixCur].rConNodes[i]._disToStart)
                {
                    rNodes[ixCur].rConNodes[i]._disToStart = dis;
                    // and save the node index, which helps us trace back the path.
                    rNodes[ixCur].rConNodes[i]._ixPrevNode = ixCur;
                }
            }

            // 9. Mark this index as having been visited.
            visited[ixCur] = true;

            // If we have visited/reached the destination node, the algorithm has finished.
            if(visited[ixGoal])
            {
                foundPath = true;
            }
        }

        // 10. Now that we have presumably found a path, we need to trace back the path that we found.
        List<AI_Node> path = new List<AI_Node>();
        ixCur = ixGoal;
        path.Add(rNodes[ixCur]);
        while(ixCur != ixStart)
        {
            ixCur = rNodes[ixCur]._ixPrevNode;
            if(ixCur == -1){
                // means that there is no valid path.
                return null;
            }
            path.Add(rNodes[ixCur]);
        }

        // since we went goal -> intermediates -> start, we need to flip the order of everything around.
        path.Reverse();
        return path;
    }

    private int FindSmallestUnvisitedNode(bool[] visitedIndexes, AI_Node[] nodes)
    {
        float nodeDis = 1000000f;
        int curNode = -1;
        for(int i=0; i<nodes.Length; i++){
            if(visitedIndexes[i]){
                continue;
            }
            // this is not a visited node
            else{
                float heuristicDis = nodes[i]._disToStart + nodes[i]._disToGoal;
                // getting the node with the shortest distance
                if(heuristicDis < nodeDis){
                    nodeDis = nodes[i]._disToStart;
                    curNode = i;
                }
            }
        }

        if(curNode == -1){
            Debug.Log("Woah, for some reason we couldn't find an appropriate node");
        }

        // check if the returned node is equal to our goal node in the function that calls this.
        return curNode;
    }
}
