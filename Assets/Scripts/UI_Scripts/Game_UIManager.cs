using UnityEngine;
using System.Collections;

public class Game_UIManager : MonoBehaviour {

	GameObject cursor;
	GameObject mainCam;
	Player player;
	Material cursorMat;



	void Start(){
		cursor = GameObject.FindWithTag ("Cursor");
		cursorMat = cursor.GetComponent<Renderer> ().material;
		mainCam = GameObject.FindWithTag ("MainCamera");
		player = Player.Instance;
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

		if (cursorMat != null) {
			float p = player.getHealthPercentage();
			Color c = cursorMat.color;
			c.r = 1 - p;
			c.g = p;
			c.b = p/2;
			cursorMat.color = c;


		}
	}

}
