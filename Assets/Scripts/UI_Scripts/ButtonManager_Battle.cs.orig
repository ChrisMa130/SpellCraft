using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * This subclass of ButtonManager_Menu handles the listeners 
 * of the buttons in the page "How to Battle"
 * */
public class ButtonManager_Battle : ButtonManager_Menu {

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
