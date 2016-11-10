using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_UIManager : MonoBehaviour {

	GameObject cursor;
	GameObject mainCam;
	HoloToolkit.Unity.BasicCursor basicCursor;
    CursorColor cursorColor;
	Player player;




	void Start(){
		cursor = GameObject.FindWithTag ("Cursor");
		basicCursor = cursor.GetComponent<HoloToolkit.Unity.BasicCursor> ();
        cursorColor = cursor.GetComponent<CursorColor>();

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
		updateCursorManaBalls ();
		
	}


	public void updateCursorColor(){
		float p = player.getHealthPercentage();
        cursorColor.set_hp(p);

	}

	public void updateCursorManaBalls(){
		int mp = player.getMagic ();
        cursorColor.set_mp(mp);
	}


}
