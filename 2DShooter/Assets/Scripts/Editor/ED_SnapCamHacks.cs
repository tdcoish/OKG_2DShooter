using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ED_SnapCamHacks : MonoBehaviour {

	// // Store new position in the mn_SnapCamera script.
	// // Must be one in the scene
	// [MenuItem("SnapCamHacks/StoreNewPosition")]
	// private static void StoreNewPosition(){
	// 	PosAndRot temp = new PosAndRot();
	// 	Quaternion rot = SceneView.lastActiveSceneView.camera.transform.rotation;
	// 	Vector3 pos = SceneView.lastActiveSceneView.camera.transform.position;

	// 	Transform[] cam = Selection.transforms;
	// 	if(cam.Length != 0){
	// 		var snapCam = cam[0].GetComponent<MN_SnapCamera>();
	// 		Undo.RecordObject(snapCam, "Added a camera spot");

	// 		temp.pos = pos;
	// 		temp.rot = rot.eulerAngles;

	// 		snapCam.mPosAndRots.Add(temp);

	// 		PrefabUtility.RecordPrefabInstancePropertyModifications(snapCam);
	// 	}

	// }

}
