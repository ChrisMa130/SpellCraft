using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {
	public GameObject[] Spells;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CastSpell(int index) {
		Vector3 offset = Camera.main.transform.forward * 0.5f;
		GameObject spell = Instantiate (Spells [index], Camera.main.transform.position + offset, Camera.main.transform.rotation) as GameObject;
		spell.GetComponent<EffectSettings> ().Target = GameObject.Find ("BasicCursor");
	}
}
