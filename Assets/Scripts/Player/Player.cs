using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	private float healthPoint = 10f;
	private float magicPoint = 10f;

	private bool gameStarted = false;  // once network connected, set to true
	private bool singlePlayer = false;

	// Use this for initialization
	void Start () {
		// set single player and gamestarted to true if on single player mode
	}
	
	// Update is called once per frame
	void Update () {
		// single player mode
		if (singlePlayer) {
			if (healthPoint <= 0) {
				SceneManager.LoadScene (0);
				//Application.loadedLevel(null);
				Destroy (gameObject);
			}
		} 
		// multiplayer mode
		else {
			if (gameStarted) {
				if (healthPoint <= 0) {
					gameStarted = false;

					//network code here
					SceneManager.LoadScene(0);
					//Application.LoadLevel ();
					Destroy (gameObject);
				}
			} else {
				SceneManager.LoadScene (0);
				//Application.LoadLevel (null);
				Destroy (gameObject);
			}
		}
	}

	// subtract health from spell damage (call by Spell)
	public void ReduceHealth(float damage) {
		healthPoint -= damage;
	}

	// decrease mp from spell cost
	public void ReduceMagic(float mp) {
		magicPoint -= mp;
	}

	// increase magic from orbs (call by Orb)
	public void GainMP(float mp) {
		magicPoint += mp;
	}
}
