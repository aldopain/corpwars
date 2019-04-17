using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_EventPool : MonoBehaviour {
	public SystemEvent_Trade tradeDealMade;
	public SystemEvent_Fight fightStarted;
	public SystemEvent_Fight fightEnded;
	public SystemEvent_Caravan caravanCreated;
	public SystemEvent_Caravan caravanDestroyed;
	public SystemEvent_Diplomacy warDeclared;
	public SystemEvent_Diplomacy peaceDeclared;
	public SystemEvent_Diplomacy nonWarPactDeclared;
	public SystemEvent_Diplomacy tradePactDeclared;
	public SystemEvent_Diplomacy nonWarPactRuined;
	public SystemEvent_Diplomacy tradePactRuined;
}
