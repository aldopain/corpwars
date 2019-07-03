using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piqUI_ShipWindowManager : MonoBehaviour
{
    //Units and buildings
    [Header("Orders")]
    public List<Button> ButtonGrid; //Should probably be its own class, but it'll do for now

    [Header("Trade")]
    public List<piqUI_TradeNodePanel> TradePanels;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
