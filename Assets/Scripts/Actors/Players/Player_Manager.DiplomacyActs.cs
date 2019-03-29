using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : MonoBehaviour {
	public SystemEvent_Diplomacy warDeclared;
	public SystemEvent_Diplomacy peaceDeclared;
	public SystemEvent_Diplomacy nonWarPactDeclared;
	public SystemEvent_Diplomacy tradePactDeclared;
	public SystemEvent_Diplomacy nonWarPactRuined;
	public SystemEvent_Diplomacy tradePactRuined;

	public void DeclareWar(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.WAR);
		pm2.relations.SetStatus(this, Player_Relations.WAR);

		this.relations.ChangeRelations(pm2, -200);
		pm2.relations.ChangeRelations(this, -200);

		warDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredWar);
	}

	public void DeclarePeace(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NEUTRAL);
		pm2.relations.SetStatus(this, Player_Relations.NEUTRAL);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);

		peaceDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredPeace);
	}

	public void DeclareTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, Player_Relations.TRADE_PACT);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);

		tradePactDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredTradePact);
	}

	public void RuinTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, -Player_Relations.TRADE_PACT);

		this.relations.ChangeRelations(pm2, -50);
		pm2.relations.ChangeRelations(this, -50);

		tradePactRuined.Invoke(this, pm2, Player_Actions.Action.RuinedTradePact);
	}

	public void DeclareNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, Player_Relations.NO_WAR_DECLARE);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);

		nonWarPactDeclared.Invoke(this, pm2, Player_Actions.Action.DeclaredNonWarPact);
	}

	public void RuinNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, -Player_Relations.NO_WAR_DECLARE);

		this.relations.ChangeRelations(pm2, -50);
		pm2.relations.ChangeRelations(this, -50);

		nonWarPactRuined.Invoke(this, pm2, Player_Actions.Action.RuinedNonWarPact);
	}
}
