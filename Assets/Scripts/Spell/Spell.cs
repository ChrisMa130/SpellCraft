using UnityEngine;
using System.Collections;

public interface Spell : MonoBehaviour {

	// get damage of spell
	float getDamage() {}

	// get damage of spell
	float getMPCost() {}

	// reduce Player health on collision, and destroy spell instance
	void OnCollisionEnter(Collider collision) {}
}