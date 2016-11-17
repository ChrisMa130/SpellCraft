using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This subclass of ButtonManager_Menu handles the listeners 
 * of the buttons in the page "Rescan Area"
 * */
public class ButtonManager_Rescan : ButtonManager_Menu {

	// Use this for initialization
	void Start () {

		base.Start ();
		foreach (Button btn in components)
		{

			switch (btn.name)
			{
			case "Menu": btn.onClick.AddListener(onBtnMenuClick);  break;

			default: break;
			}
		}
	}


    private void onBtnMenuClick() {
        menuMgr.turnOffScanning();
        menuMgr.openUI(0);
    }
	

}
