/*************************************************************************************

*************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class UI_PC : MonoBehaviour
{

    public Image                            _healthBar;

    public void FSetBarSize(float percZeroToOne)
    {
        _healthBar.fillAmount = percZeroToOne;
    }
}
