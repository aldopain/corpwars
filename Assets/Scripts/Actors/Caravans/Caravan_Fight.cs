using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Fight {

	public static IEnumerator Start (GameObject attacker, GameObject defender){
		bool atIsAlive = true, defIsAlive = true;

		var attackerAggro = attacker.GetComponent<Caravan_Aggro>();
		var defenderAggro = defender.GetComponent<Caravan_Aggro>();

		attackerAggro.IsInFight = true;
		defenderAggro.IsInFight = true;

		var attackerMovement = attacker.GetComponent<Actor_CaravanMovement>();
		var defenderMovement = defender.GetComponent<Actor_CaravanMovement>();

		attackerMovement.ChangeIsStopped(true);
		defenderMovement.ChangeIsStopped(true);

		var defShips = defender.GetComponent<Caravan_UnitManager>();
		var atShips = attacker.GetComponent<Caravan_UnitManager>();

		// copies for statistics after fight
		var dsCopy = new List<Ship_BaseInfo>(defShips.ShipList);
		var asCopy = new List<Ship_BaseInfo>(atShips.ShipList);

		// main logic of cleaning lists from dead AFTER TWO iterations is
		// that ships attack at the one moment
		while (atIsAlive || defIsAlive){
			Round(atShips, defShips);
			Round(defShips, atShips);
			End(atShips, defShips);
			atIsAlive = atShips.IsAlive();
			defIsAlive = defShips.IsAlive();
		}

		yield return new WaitForSeconds(5);

		if (!defIsAlive) GameObject.Destroy(defender);
		if (!atIsAlive) GameObject.Destroy(defender);

		// TODO: some stuff like statistics, rewards, exp, etc.

		attackerAggro.IsInFight = false;
		defenderAggro.IsInFight = false;
		
		attackerMovement.ChangeIsStopped(false);
		defenderMovement.ChangeIsStopped(false);
	}

	static void Round(Caravan_UnitManager attacker, Caravan_UnitManager defender){
		foreach (var ship in attacker.ShipList){
			if (ship.Health > 0)
				AttackRandomEnemy(ship, defender);
		}
	}

	static void AttackRandomEnemy(Ship_BaseInfo unit, Caravan_UnitManager enemy){
		enemy.ShipList[Random.Range(0, enemy.ShipList.Count)].Health -= unit.Attack;
	}

	static void End(Caravan_UnitManager a, Caravan_UnitManager b){
		ClearFromDead(a);
		ClearFromDead(b);
	}

	static void ClearFromDead(Caravan_UnitManager cum){
		var cleanList = new List<Ship_BaseInfo>();
		foreach (var ship in cum.ShipList)
			if (ship.Health > 0)
				cleanList.Add(ship);
		cum.ShipList = cleanList;
	}
}
