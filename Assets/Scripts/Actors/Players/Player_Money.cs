using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Money : MonoBehaviour {
    /// <summary>
    /// Current amount of money this player has
    /// </summary>
    public int Amount;

    /// <summary>
    /// Add a set amount of money to this player 
    /// </summary>
    /// <param name="a">Amount of money to add</param>
	public void Add(int a){
		Amount += Mathf.Abs(a);
	}

    /// <summary>
    /// Detract a set amount of money from this player whithout any checks
    /// </summary>
    /// <param name="a">Amount of money to detract</param>
	public void Detract(int a){
		Amount -= Mathf.Abs(a);
	}

    public bool Detract_Safe(int a)
    {
        if (Check(a))
        {
            Detract(a);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Set amount of money
    /// </summary>
    /// <param name="a"></param>
	public void Set(int a){
		Amount = a;
	}

    /// <summary>
    /// Check if this player has enough money
    /// </summary>
    /// <param name="a">Amount of money to check</param>
    /// <returns><c>true</c> if this player has enough money</returns>
	public bool Check(int a){
		if(Amount >= a) return true;
		return false;
	}

    /// <summary>
    /// Transfer money from this player to another
    /// </summary>
    /// <param name="a">Amount of money to transfer</param>
    /// <param name="other">Player, who recieves the money</param>
	public void Transfer(int a, Player_Money other){
		if(!other.Check(a)) return;
		Detract(a);
		other.Add(a);
	}
}
