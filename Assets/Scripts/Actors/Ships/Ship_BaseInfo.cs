using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShip",menuName = "Ship Prefab")]
public class Ship_BaseInfo:ScriptableObject{
    public string Name;
    public float Speed = 1;
    public double InventorySize;
    public double Attack;
    public double Defence;
    public double CurrentHealth = 1;
    public double MaximumHealth = 1;
    public int Maintenance = 1;
    public int Cost = 1;
    public Mesh mesh;

    
	public int GetRepairCost(){
        return (int) ((MaximumHealth - CurrentHealth) * 0.1 * Maintenance);
	}

	public void Repair(Player_Manager pm){
        var playerMoney = pm.GetComponent<Player_Money>();
        Repair(playerMoney);
	}

    public void Repair(Player_Money playerMoney){
		CurrentHealth = MaximumHealth;
        var repairCost = GetRepairCost();
        if (playerMoney.Amount > repairCost)
            playerMoney.Add(-repairCost);
	}
}
