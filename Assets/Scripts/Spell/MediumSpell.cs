public class MediumSpell : Monobehavior, Spell {
	// stats of the medium spell
	private string spellName = "incendio";
	private int damage = 25;
	private int mpCost = 5;

	// get damage of spell
	public int getDamage() {
		return this.damage;
	}

	// get damage of spell
	public int getMPCost() {
		return this.mpCost;
	}

	// reduce Player health on collision, and destroy spell instance
	public void OnCollisionEnter(Collision other) {
		if (other.collier.tag.Equals("MainComera")){}
			// reduce player health
			other.gameObject.GetComponent<Player>().ModifyHealth(this.getDamage());
		}
	}
}