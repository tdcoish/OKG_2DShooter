/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;


public class DEATH_Man : MonoBehaviour
{
    public void BT_PlayAgain()
    {
        SceneManager.LoadScene("Arena");
    }
}
