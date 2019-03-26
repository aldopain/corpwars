using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class xUI_PointerEvents : MonoBehaviour {
    public UnityEvent OnMouseEnterEvent;
    public UnityEvent OnMouseExitEvent;
    public UnityEvent OnClick;
    public UnityEvent OnAltClick;

    void OnMouseEnter()
    {
        print(name + " Mouse Enter");
        OnMouseEnterEvent.Invoke();
    }

    void OnMouseExit()
    {
        print(name + " Mouse Exit");
        OnMouseExitEvent.Invoke();
    }

    void OnMouseOver()
    {
        print(name + " Mouse Over");
        if (Input.GetMouseButtonDown(0)) OnClick.Invoke();
        else if (Input.GetMouseButtonDown(1)) OnAltClick.Invoke();
    }
}
