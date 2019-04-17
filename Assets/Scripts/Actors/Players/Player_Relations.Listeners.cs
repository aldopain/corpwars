using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Relations : MonoBehaviour {
  void ListenAll(){
    pm.events.warDeclared.AddListener(DiplomacyActInvoked);
    pm.events.peaceDeclared.AddListener(DiplomacyActInvoked);
    pm.events.nonWarPactDeclared.AddListener(DiplomacyActInvoked);
    pm.events.tradePactDeclared.AddListener(DiplomacyActInvoked);
    pm.events.nonWarPactRuined.AddListener(DiplomacyActInvoked);
    pm.events.tradePactRuined.AddListener(DiplomacyActInvoked);
  }

  int CountRelationsAffection(Player_Actions.Action action){
    switch (action)
    {
      case Player_Actions.Action.DeclaredWar:
        return -150;
      case Player_Actions.Action.DeclaredPeace:
        return 50;
      case Player_Actions.Action.DeclaredTradePact:
        return 50;
      case Player_Actions.Action.RuinedTradePact:
        return -50;
      case Player_Actions.Action.DeclaredNonWarPact:
        return 50;
      case Player_Actions.Action.RuinedNonWarPact:
        return -50;
      case Player_Actions.Action.TradeDealMade:
        return 25;
      default:
        return 0;
    }
  }

  void DiplomacyActInvoked(Player_Manager pm1, Player_Manager pm2, Player_Actions.Action action){
    if (pm1.Equals(pm)){
      pm1.relations.ChangeRelations(pm2, CountRelationsAffection(action));
    } else if (pm2.Equals(pm)){
      pm2.relations.ChangeRelations(pm1, CountRelationsAffection(action));
    }
  }
}
