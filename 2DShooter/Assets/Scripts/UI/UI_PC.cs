/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class UI_PC : MonoBehaviour
{
    public Image                            _ammoBar;
    public Image                            _healthBar;
    public Image                            _shieldBar;
    public Text                             _scoreText;
    public Text                             _timeText;

    public void FSetBarSize(float percZeroToOne)
    {
        _healthBar.fillAmount = percZeroToOne;
    }

    public void FSetAmmoBarSize(int curAmmo, int maxAmmo)
    {
        float perc = (float)curAmmo / (float)maxAmmo;
        _ammoBar.fillAmount = perc;
    }

    public void FSetShieldBarSize(float curVal, float maxVal)
    {
        float perc = curVal/maxVal;
        _shieldBar.fillAmount = perc;
    }

    public void FSetScoreText(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }

    public void FSetTimeText(float time)
    {
        // convert this to minutes and seconds.
        int sec = (int)time % 60;
        int min = (int)time / 60;
        _timeText.text = min.ToString() + ":" + sec.ToString();
    }
}
