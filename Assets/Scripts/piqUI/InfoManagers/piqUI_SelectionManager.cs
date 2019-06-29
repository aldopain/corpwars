using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class piqUI_SelectionManager : MonoBehaviour
{
    private GameObject highlitedObject;
    private GameObject selectedObject;

    public enum SelectedType {
        Caravan,
        Town
    }

    public UnityEvent OnNewSelection;
    private piqUI_InformationManager manager;

    void Start() {
        manager = GetComponent<piqUI_InformationManager>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            highlitedObject = null;
            manager.windows.currentWindow.Hide();
        }
    }

    public void SetSelectedObject(GameObject go) {
        selectedObject = go;
        switch (selectedObject.tag) {
            case "Caravan":
                OnSelectCaravan();
                break;
            case "Town":     
                OnSelectTown();
                break;
        }
    }

    public void SetHighlightedObject(GameObject go) {
        highlitedObject = go;
    }

    //Turn on town window and update info
    private void OnSelectTown() {
        //This is a test line, window should probably be attached to something else
        manager.windows.ShowWindow(manager.windows.TownWindow);
        manager.towns.SetSelectedTown(selectedObject);
    }

    private void OnSelectCaravan() {

    }

}
