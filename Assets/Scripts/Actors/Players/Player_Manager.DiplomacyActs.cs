using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : MonoBehaviour {

	public void DeclareWar(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.WAR);
		pm2.relations.SetStatus(this, Player_Relations.WAR);

		this.relations.ChangeRelations(pm2, -200);
		pm2.relations.ChangeRelations(this, -200);
	}

	public void DeclarePeace(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NEUTRAL);
		pm2.relations.SetStatus(this, Player_Relations.NEUTRAL);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);
	}

	public void DeclareTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, Player_Relations.TRADE_PACT);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);
	}

	public void RuinTradePact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.TRADE_PACT);
		pm2.relations.SetStatus(this, -Player_Relations.TRADE_PACT);

		this.relations.ChangeRelations(pm2, -50);
		pm2.relations.ChangeRelations(this, -50);
	}

	public void DeclareNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, Player_Relations.NO_WAR_DECLARE);

		this.relations.ChangeRelations(pm2, 50);
		pm2.relations.ChangeRelations(this, 50);
	}

	public void RuinNonWarPact(Player_Manager pm2){
		this.relations.SetStatus(pm2, -Player_Relations.NO_WAR_DECLARE);
		pm2.relations.SetStatus(this, -Player_Relations.NO_WAR_DECLARE);

		this.relations.ChangeRelations(pm2, -50);
		pm2.relations.ChangeRelations(this, -50);
	}
}
