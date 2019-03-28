using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Fight {

	static float roundLength = 1f;

	public static IEnumerator Start (GameObject attacker, GameObject defender){
		bool atIsAlive = true, defIsAlive = true;

		var attackerAggro = attacker.GetComponent<Caravan_Aggro>();
		var defenderAggro = defender.GetComponent<Caravan_Aggro>();

		attackerAggro.IsInFight = true;
		defenderAggro.IsInFight = true;

		var attackerMovement = attacker.GetComponent<Actor_CaravanMovement>();
		var defenderMovement = defender.GetComponent<Actor_CaravanMovement>();

		TryChangeIsStopped(attackerMovement, true);
		TryChangeIsStopped(defenderMovement, true);

		var defShips = defender.GetComponent<Caravan_UnitManager>();
		var atShips = attacker.GetComponent<Caravan_UnitManager>();

		// copies for statistics after fight
		var dsCopy = new List<Ship_BaseInfo>(defShips.ShipList);
		var asCopy = new List<Ship_BaseInfo>(atShips.ShipList);

		// main logic of cleaning lists from dead AFTER TWO iterations is
		// that ships attack at the one moment
		while (atIsAlive && defIsAlive){
			Round(atShips, defShips);
			Round(defShips, atShips);
			End(atShips, defShips);
			atIsAlive = atShips.IsAlive();
			defIsAlive = defShips.IsAlive();
			Debug.Log("Next round!");
			yield return new WaitForSeconds(1);
		}

		Debug.Log("End of fight");

		QuitFromFight(attacker, attackerAggro, attackerMovement, atIsAlive);
		QuitFromFight(defender, defenderAggro, defenderMovement, defIsAlive);
		if (atIsAlive && !defIsAlive)
			TryConquerorPoint(attacker, defender, defenderMovement);

		// TODO: some stuff like statistics, rewards, exp, etc.
	}

	static void QuitFromFight (GameObject go, Caravan_Aggro aggro, Actor_CaravanMovement movement, bool IsAlive){
		if (!IsAlive && movement != null) {
			GameObject.Destroy(go);
		} else {
			aggro.IsInFight = false;
			TryChangeIsStopped(movement, false);
		}
	}

	static void TryConquerorPoint(GameObject attacker, GameObject defender, Actor_CaravanMovement defenderMovement){
		if (defenderMovement == null) {
			defender.GetComponent<Actor_Resources>().Owner = attacker.GetComponent<Actor_Resources>().Owner;
		}
	}

	static void TryChangeIsStopped(Actor_CaravanMovement movement, bool value){
		if (movement != null)
			movement.ChangeIsStopped(false);
	}

	static void Round(Caravan_UnitManager attacker, Caravan_UnitManager defender){
		foreach (var ship in attacker.ShipList){
			if (ship.Health > 0)
				AttackRandomEnemy(ship, defender);
		}
	}

	static void AttackRandomEnemy(Ship_BaseInfo unit, Caravan_UnitManager enemy){
		var enemyUnit = enemy.ShipList[Random.Range(0, enemy.ShipList.Count)];
		enemyUnit.Health -= unit.Attack * roundLength * (100 - enemyUnit.Defence) / 100;
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
