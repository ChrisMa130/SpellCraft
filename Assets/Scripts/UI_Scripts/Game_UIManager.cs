using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_UIManager : MonoBehaviour {

	GameObject cursor;
	GameObject mainCam;
	HoloToolkit.Unity.BasicCursor basicCursor;
	Player player;
	Material cursorMat;
	Mesh[] manaBalls;
	Transform manaBall_1;




	void Start(){
		cursor = GameObject.FindWithTag ("Cursor");
		basicCursor = cursor.GetComponent<HoloToolkit.Unity.BasicCursor> ();
		cursorMat = cursor.GetComponent<Renderer> ().material;

		mainCam = GameObject.FindWithTag ("MainCamera");
		player = Player.Instance;
		//manaBalls = cursor.GetComponentsInChildren<Mesh>();

		//Debug.Log ("mana Balls: " + manaBalls.Length);
		manaBall_1 = cursor.gameObject.transform.GetChild(0);

		Renderer test = manaBall_1.GetComponent<Renderer> ();

		if (test == null) {
			Debug.Log ("test ball 1 is null");
		} else {
			Debug.Log ("test bull 1 exists");
		}

		Color c = test.material.color;

		c.r = 1f;
		c.g = 0f;
		c.b = 0f;

		test.material.color = c;



	}

	void Awake()
	{
		checkEventSystem();
	}

	void checkEventSystem()
	{
		var ESObj = GameObject.Find("EventSystem");
		if (ESObj == null)
		{
			ESObj = new GameObject("EventSystem");
		}
	}

	void Update(){
		updateCursorColor ();
		
	}


	public void updateCursorColor(){
		float p = player.getHealthPercentage();
		basicCursor.updateColor(p);

	}


}
