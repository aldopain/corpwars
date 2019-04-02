using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xUI_WindowTabs : MonoBehaviour {
    public List<xUI_Tab> Tabs;
    public xUI_Tab selectedTab;

    void Start()
    {
        foreach(xUI_Tab tab in Tabs)
        {
            tab.Disable();
        }
        selectedTab = Tabs[0];
        SelectTab(0);
    }

    public void SelectTab(int index)
    {
        SelectTab(Tabs[index]);
    }

    public void SelectTab(xUI_Tab tab)
    {
        selectedTab.Disable();
        tab.Enable();
        selectedTab = tab;
    }
}
