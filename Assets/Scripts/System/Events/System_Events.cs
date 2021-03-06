﻿using UnityEngine.Events;
using UnityEngine;

public class SystemEvent_Diplomacy: UnityEvent<Player_Manager, Player_Manager, Player_Actions.Action> {}
public class SystemEvent_Trade: UnityEvent<Player_Trade, Player_Trade, Player_Actions.Action> {}
public class SystemEvent_Fight: UnityEvent<GameObject, GameObject> {}
public class SystemEvent_Caravan: UnityEvent<GameObject> {}