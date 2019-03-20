using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : MonoBehaviour {
	public void TransferMoney(int amount, Player_Manager other) {
		this.money.Transfer(amount, other.money);
		this.relations.ChangeRelations(other, 10);
		other.relations.ChangeRelations(this, 10);
	}

	public void TransferResources(int index, double amount, Player_Manager other) {
		this.resources.Transfer(index, amount, other.resources);
		this.relations.ChangeRelations(other, 10);
		other.relations.ChangeRelations(this, 10);
	}

	public void TransferTradePost(NavigationNode post, Player_Manager other) {
		if (tradePosts.Contains(post)) {
			tradePosts.Remove(post);
			other.tradePosts.Add(post);
			post.resources.Owner = other;
			this.relations.ChangeRelations(other, 50);
			other.relations.ChangeRelations(this, 50);
		}
	}
}