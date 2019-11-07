/*************************************************************************************

*************************************************************************************/
using UnityEngine;

public class UI_CrossHair : MonoBehaviour
{

    void Update()
    {
 		Camera c = Camera.main;
		Vector2 msPos = c.ScreenToWorldPoint(Input.mousePosition);
		transform.position = msPos;       
    }
}
