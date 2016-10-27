using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	private string spellName = null;
	private float damage = 1f;
	private float mpCost = 1f;
	private GameObject spellEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* constructor
	void newSpell(String spellName, float damage, float mpCost, GameObject spellEffect) {
		this.spellName = spellName;
		this.damage = damage;
		this.mpCost = mpCost;
		this.spellEffect = spellEffect;
	}*/

	public float getDamage() {
		return this.damage;
	}

	// reduce Player health on collision, and destroy spell instance
	void OnCollisionEnter(Collision collision) {
		// reduce player health
		collision.gameObject.GetComponent<Player>().ReduceHealth(getDamage());
		//Destroy (gameObject); Not needed, will get rid of explosion animation.
	}

	// destroy spell after hitting room boundary
	void OnTriggerEnter(Collider collision) {
		
	}

	// display spell Effect
	void displayEffect(){
		Vector3 offset = Camera.main.transform.forward * 0.5f;
		if (spellEffect != null) {
			GameObject spell = Instantiate(spellEffect, Camera.main.transform.position + offset, Camera.main.transform.rotation) as GameObject;
			spell.GetComponent<EffectSettings> ().Target = GameObject.Find ("BasicCursor");
		}
	}
}