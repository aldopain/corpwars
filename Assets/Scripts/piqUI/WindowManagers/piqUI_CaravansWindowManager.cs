using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piqUI_CaravansWindowManager : piqUI_Window {
    public Text CaravanList;    //It will do for now, should be replaced by its own widget
    public piqUI_InformationManager manager;

    void Start() {
        Show();
    }

    public override void Show() {
        ShowCaravans();
        base.Show();
    }

    void ShowCaravans() {
        CaravanList.text = manager.caravans.PrintCaravans();
    }
}
