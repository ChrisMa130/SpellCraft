using UnityEngine;
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
				Application.loadedLevel (LoseScene);
				Destroy (gameObject);
			}
		} 
		// multiplayer mode
		else {
			if (gameStarted) {
				if (healthPoint <= 0) {
					gameStarted = false;

					//network code here

					Application.LoadLevel (LoseScene);
					Destroy (gameObject);
				}
			} else {
				Application.LoadLevel (WinScene);
				Destroy (gameObject);
			}
		}
	}

	// subtract health from spell damage (call by Spell)
	public void ReduceHealth(float damage) {
		healthPoint -= damgage;
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
