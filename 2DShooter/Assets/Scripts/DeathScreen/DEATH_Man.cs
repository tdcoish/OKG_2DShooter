/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DEATH_Man : MonoBehaviour
{
    public Text                                 _txtScore;

    void Start()
    {
        _txtScore.text = "SCORE: " + GB_Score._score;
    }

    public void BT_PlayAgain()
    {
        SceneManager.LoadScene("Arena");
    }
}
