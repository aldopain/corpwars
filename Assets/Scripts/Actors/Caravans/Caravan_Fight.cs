using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Fight {

	public static void Start (GameObject attacker, GameObject defender){
		var defShips = defender.GetComponent<Caravan_UnitManager>();
		var atShips = attacker.GetComponent<Caravan_UnitManager>();

		// copies for statistic after fight
		var dsCopy = new List<Ship_BaseInfo>(defShips.ShipList);
		var asCopy = new List<Ship_BaseInfo>(atShips.ShipList);

		// main logic of cleaning lists from dead AFTER TWO iterations is
		// that ships attack at the one moment
		while (defShips.IsAlive() || atShips.IsAlive()){
			Round(atShips, defShips);
			Round(defShips, atShips);
			End(atShips, defShips);
		}
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
			if (ship.Health > 0) cleanList.Add(ship);
		cum.ShipList = cleanList;
	}


}
