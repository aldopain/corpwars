using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqUI_WindowManager : MonoBehaviour
{
    public piqUI_Window currentWindow;

    public piqUI_Window TownWindow;


    public void ShowWindow(piqUI_Window window) {
        currentWindow = window;
        window.Show();
    }
}
