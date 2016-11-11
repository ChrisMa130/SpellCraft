using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This subclass of ButtonManager_Menu handles the listeners 
 * of the buttons in the page "How to Start Playing"
 * */
public class ButtonManager_StartPlaying : ButtonManager_Menu {

	// Use this for initialization
	void Start () {

		base.Start ();
		foreach (Button btn in components)
		{

			switch (btn.name)
			{
			case "Back": btn.onClick.AddListener(onBtnBackClick);  break;

			default: break;
			}
		}
	}
}
