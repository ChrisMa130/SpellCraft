using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	private string spellName = null;
	private int damage = 0;
	private int mpCost = 0;

	// get damage of spell
	public int getDamage() {
		return this.damage;
	}

	// get damage of spell
	public float getMPCost() {
		return this.mpCost;
	}

	// reduce Player health on collision, and destroy spell instance
	void OnTriggerEnter(Collider collision) {
		// reduce player health
		collision.gameObject.GetComponent<Player>().modifyHealth(this.getDamage());
	}
}