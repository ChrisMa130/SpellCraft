public class HeavySpell : Monobehavior, Spell {
	// stats of the heavy spell
	private string spellName = "Heavy Spell";
	private float damage = 50f;
	private float mpCost = 10f;

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