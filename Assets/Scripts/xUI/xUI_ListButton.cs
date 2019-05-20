using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xUI_ListButton : MonoBehaviour {
    public Text ButtonText;
    public Button ActionButton;
    public Button CloseButton;

    public void Close() {
        Destroy(gameObject);
    }
}
