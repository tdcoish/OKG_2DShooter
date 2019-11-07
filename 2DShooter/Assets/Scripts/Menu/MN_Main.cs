/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MN_Main : MonoBehaviour
{
    public void BT_Play()
    {
        SceneManager.LoadScene("Arena");
    }
    public void BT_Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void BT_Quit()
    {
        Application.Quit();
    }
}
