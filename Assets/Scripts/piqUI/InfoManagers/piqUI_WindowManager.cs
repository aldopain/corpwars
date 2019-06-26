using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqUI_WindowManager : MonoBehaviour
{
    public IWindow currentWindow;

    public piqUi_TownWindowManager TownWindow;

    public void ShowWindow(IWindow window) {
        currentWindow = window;
        window.Show();
    }
}
