using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCursorControl : MonoBehaviour
{
    public GameObject cursorActive;
    public GameObject cursorDisabled;

    private bool isCursorActive;
    private bool activated = false;

    private void Awake()
    {
        cursorActive.SetActive(false);
        cursorDisabled.SetActive(false);
    }

    /// <summary>
    /// Sent by the Player
    /// 
    /// Checks which cursor to activate
    /// </summary>
    public void CursorActive(bool active)
    { 
        if (active != isCursorActive || !activated)
        {
            activated = true;
            if (active)
            {
                cursorActive.SetActive(true);
                cursorDisabled.SetActive(false);
                isCursorActive = active;
            }
            else
            {
                cursorActive.SetActive(false);
                cursorDisabled.SetActive(true);
                isCursorActive = false;
            }
        }
    }
}
