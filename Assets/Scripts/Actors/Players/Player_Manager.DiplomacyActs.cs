using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : Player_Trade {

	public void DeclareWar(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.WAR);
		pm2.relations.SetStatus(this, Player_Relations.WAR);

		events.warDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredWar);
	}

	public void DeclarePeace(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NEUTRAL);
		pm2.relations.SetStatus(this, Player_Relations.NEUTRAL);

		events.peaceDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredPeace);
	}

	public void DeclareTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, Player_Relations.TRADE_PACT);

		events.tradePactDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredTradePact);
	}

	public void RuinTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, -Player_Relations.TRADE_PACT);

		events.tradePactRuined.Invoke(this, pm2, Player_Actions.Action.RuinedTradePact);
	}

	public void DeclareNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, Player_Relations.NO_WAR_DECLARE);

		events.nonWarPactDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredNonWarPact);
	}

	public void RuinNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, -Player_Relations.NO_WAR_DECLARE);

		events.nonWarPactRuined.Invoke(this, pm2, Player_Actions.Action.RuinedNonWarPact);
	}
}
