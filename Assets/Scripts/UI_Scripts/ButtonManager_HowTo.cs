using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    /**
     * This subclass of ButtonManager_Menu handles the listeners 
     * of the buttons in the page "Hot To Play"
     * */
public class ButtonManager_HowTo : ButtonManager_Menu {

	void Start () {
		base.Start ();

		foreach (Button btn in components)
		{

			switch (btn.name)
			{
			case "Battle": btn.onClick.AddListener(onBtnBattleClick);  break;
			case "StartPlaying": btn.onClick.AddListener(onBtnStartPlayingClick);  break;
			case "Back": btn.onClick.AddListener(onBtnBackClick);  break;

			default: break;
			}
		}
	}

	private void onBtnBattleClick(){
		bool flag = menuMgr.openUI (3);
	}

	private void onBtnStartPlayingClick(){
		bool flag = menuMgr.openUI (4);
	}
	

}
