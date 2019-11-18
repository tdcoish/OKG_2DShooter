/************************************************************
UI for the win screen.
After a while we should display the credits.
************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class WS_UI : MonoBehaviour
{

    public Text                         _instr;

    void Start()
    {
        Color c = _instr.color;
        c.a = 0f;
        _instr.color = c;
    }

    public void FShowInstr()
    {
        Color c = _instr.color;
        c.a = 1f;
        _instr.color = c;
    }

}
