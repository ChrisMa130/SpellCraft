public class LightSpell : Monobehavior, Spell {
	// stats of the light spell
	private string spellName = "Light Spell";
	private float damage = 5f;
	private float mpCost = 1f;

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