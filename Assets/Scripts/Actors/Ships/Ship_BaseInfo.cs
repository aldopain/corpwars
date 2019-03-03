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
    public double Health = 1;
    public Mesh mesh;
}
