/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class UI_PC : MonoBehaviour
{
    public Image                            _ammoBar;
    public Image                            _healthBar;
    public Text                             _scoreText;

    public void FSetBarSize(float percZeroToOne)
    {
        _healthBar.fillAmount = percZeroToOne;
    }

    public void FSetAmmoBarSize(int curAmmo, int maxAmmo)
    {
        float perc = (float)curAmmo / (float)maxAmmo;
        _ammoBar.fillAmount = perc;
    }

    public void FSetScoreText(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }
}
