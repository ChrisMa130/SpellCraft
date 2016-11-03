public class MediumSpell : Monobehavior, Spell {
	// stats of the medium spell
	private string spellName = "Medium Spell";
	private float damage = 25f;
	private float mpCost = 5f;

	// get damage of spell
	public float getDamage() {
		return this.damage;
	}

	// get damage of spell
	public float getMPCost() {
		return this.mpCost;
	}

	// reduce Player health on collision, and destroy spell instance
	private void OnCollisionEnter(Collider collision) {
		// reduce player health
		collision.gameObject.GetComponent<Player>().ReduceHealth(getDamage());
	}
}