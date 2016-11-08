using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager_Battle : ButtonManager_Menu {

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
