#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/***************************************************************************************************
Of incredibly important not. Do not under any circumstances create objects here and add to them. Any 
object that is created here its own object. Instead, use the GameObject, and just GetComponent all the time.

The keyboard shortcut, if given, cannot be already in use by Unity. If so, the shortcut might work, or might not.
Only one of the conflicting shortcuts will run, which causes problems.
*************************************************************************************************** */
public class ED_NodeHacks
{
    // Connects arbitrary number of nodes.
    [MenuItem("EditorHacks/Connect Nodes %_g")]
    private static void ConnectNodes()
    {
        // Get selected objects
        Transform[] nodes = Selection.transforms;

        // make sure that the objects actually are nodes.
        bool theyAreNodes = true;
        for(int i=0; i<nodes.Length; i++){
            if(!nodes[i].GetComponent<AI_Node>()){
                theyAreNodes = false;
            }
        }

        if(!theyAreNodes){
            Debug.Log("At least one object is not a node");
        }else{
            // do connect nodes.
            int adds = 0;
            for(int i=0; i<nodes.Length; i++){

                var node = nodes[i].GetComponent<AI_Node>();
                Undo.RecordObject(node, "Made some connections");

                Debug.Log("Number of Nodes: " + nodes[i].GetComponent<AI_Node>().rConNodes.Count);

                for(int j=0; j<nodes.Length; j++){
                    var nodeToAdd = nodes[j].GetComponent<AI_Node>();

                    // can't connect to ourselves.
                    if(j == i) continue;

                    bool addThisNode = true;

                    // before adding the node, let's check that we haven't already added that node.
                    for(int c=0; c<nodes.Length; c++){
                        if(node.rConNodes.Contains(nodeToAdd)){
                            // then don't add this node again.
                            addThisNode = false;
                        }
                    }

                    if(addThisNode){
                        adds++;
                        var conList = node.rConNodes;
                        // Undo.RecordObject(conList, "Changing actual list");
                        node.rConNodes.Add(nodeToAdd);
                        PrefabUtility.RecordPrefabInstancePropertyModifications(node);
                    }
                }
            }
            Debug.Log("Total Adds: " + adds);            
        }
    }

    // Disconnects selected nodes from each other.
    [MenuItem("EditorHacks/Clear Node Connections %#_g")]
    private static void ClearNodeConnections()
    {
        Debug.Log("Deleting marked connections");

        Transform[] nodeObjs = Selection.transforms;

        bool theyAreNodes = true;
        for(int i=0; i<nodeObjs.Length; i++){
            if(!nodeObjs[i].GetComponent<AI_Node>()){
                theyAreNodes = false;
            }
        }

        if(!theyAreNodes){
            Debug.Log("At least one object is not a node");
        }else{
            // do disconnect nodes.
            for(int i=0; i<nodeObjs.Length; i++){
                var node = nodeObjs[i].GetComponent<AI_Node>();
                Undo.RecordObject(node, "Deleting marked connections");

                for(int j=0; j<nodeObjs.Length; j++){
                    // can't disconnect from ourselves.
                    if(j == i) continue;

                    // also make sure that we actually have the node to remove first
                    bool hasNode = node.rConNodes.Contains(nodeObjs[j].GetComponent<AI_Node>());

                    if(hasNode){
                        node.rConNodes.Remove(nodeObjs[j].GetComponent<AI_Node>());
                        PrefabUtility.RecordPrefabInstancePropertyModifications(node);                        
                    }else{
                        Debug.Log("Didn't have that node");
                    }
                }
            }
        }
    }
    
    
    // Clears all nodes of all connections.
    [MenuItem("EditorHacks/Clear All Node Connections")]
    private static void ClearAllNodeConnections()
    {

        // Get all objects in the scene 
        List<GameObject> nodeObjs = new List<GameObject>();
        foreach(GameObject GO in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]){
            nodeObjs.Add(GO);
        }

        // Iterate through all of them and clear the nodes of all connections.
        for(int i=0; i<nodeObjs.Count; i++){
            AI_Node node = nodeObjs[i].GetComponent<AI_Node>();
            if(node){
                Undo.RecordObject(node, "Clearing every nodes connections");
                node.rConNodes.Clear();
            }
        }

    }

    // [MenuItem("EditorHacks/ShowConnections")]
    // private static void ShowConnections()
    // {
    //     // Get all objects in the scene 
    //     List<GameObject> nodeObjs = new List<GameObject>();
    //     foreach(GameObject GO in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]){
    //         nodeObjs.Add(GO);
    //     }

    //     // Iterate through all of them and show the connections.
    //     for(int i=0; i<nodeObjs.Count; i++){
    //         AI_Node node = nodeObjs[i].GetComponent<AI_Node>();
    //         if(node){
    //             for(int j=0; j<node.rConNodes.Count; j++){
    //                 Gizmos.DrawLine(node.transform.position, node.rConNodes[j].transform.position);
    //             }
    //         }
    //     }
    // }

    // This is just for me. Will never be called in the game. Destinations are found out programmatically,
    // not in the editor.
    // [MenuItem("EditorHacks/Enemy Save Node")]
    // private static void StoreDestinationNode()
    // {
    //     // necessary for actually saving the changes in the scene
    //     EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        
    //     // Get selected objects
    //     Transform[] objects = Selection.transforms;

    //     bool oneEnemy = false;
    //     bool oneNode = false;
    //     int enIndex = -1;
    //     int ndIndex = -1;

    //     // Only if there's one enemy and one node do we continue.
    //     for(int i=0; i<objects.Length; i++){
    //         if(objects[i].GetComponent<AI_Controller>()){
    //             oneEnemy = true;
    //             enIndex = i;
    //         }

    //         if(objects[i].GetComponent<AI_Node>()){
    //             oneNode = true;
    //             ndIndex = i;
    //         }
    //     }

    //     if(oneNode && oneEnemy){
    //         //objects[enIndex].GetComponent<AI_Controller>().endTarget = (AI_Node)objects[ndIndex].GetComponent<AI_Node>();
    //     }
    // }

    // // Required because if you just delete a node you'll break the references to that node.
    // [MenuItem("EditorHacks/Safe Delete Node")]
    // private static void SafeDeleteNode()
    // {
    //     EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        
    //     Transform[] objects = Selection.transforms;

    //     if(objects.Length != 1){
    //         Debug.Log("CAN ONLY DELETE 1 NODE AT A TIME");
    //         return;
    //     }

    //     if(objects[0].GetComponent<AI_Node>()){
    //         for(int i=0; i< objects[0].GetComponent<AI_Node>().rConNodes.Count; i++){
    //             // remove us from the nodes that connect to us.
    //             objects[0].GetComponent<AI_Node>().rConNodes[i].rConNodes.Remove(objects[0].GetComponent<AI_Node>());
    //             Debug.Log("REMOVED...");
    //         }

    //         // delete us from the scene.
    //         Object.DestroyImmediate(objects[0].gameObject);
            
    //     }else{
    //         Debug.Log("NODE NOT SELECTED");
    //     }
    // }
    
    // when you copy and past a node, we may want to call this is if we copied from a node with connections.
    [MenuItem("EditorHacks/Clear n Nodes List %_t")]
    private static void ClearSpecificNodesList()
    {
        Debug.Log("Clearing n node's connecting list");
        Transform[] objects = Selection.transforms;

        // first check that only nodes are selected
        for(int i=0; i<objects.Length; i++){
            if(!objects[i].GetComponent<AI_Node>()){
                Debug.Log("Not all selected are nodes");
                return;
            }
        }

        for(int i=0; i<objects.Length; i++){
            var node = objects[i].GetComponent<AI_Node>();
            Undo.RecordObject(node, "Cleaning nodes list");
            node.rConNodes.Clear();
        }

    }

    // Since sometimes this happens anyway I'll just make a function that fixes this.
    [MenuItem("EditorHacks/Clean Node References")]
    private static void CleanNodeRefs()
    {
        Debug.Log("Cleaning Node References");
        // Get all node objects in the scene 
        List<AI_Node> nodeObjs = new List<AI_Node>();
        foreach(AI_Node GO in Resources.FindObjectsOfTypeAll(typeof(AI_Node)) as AI_Node[]){
            nodeObjs.Add(GO);
        }

        // just go through and remove all null references in a list.
        for(int i=0; i<nodeObjs.Count; i++){
            Undo.RecordObject(nodeObjs[i], "Clearing all refs for this node");
            nodeObjs[i].rConNodes.RemoveAll(item => item == null);
        }
    }

}
#endif