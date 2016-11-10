using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonManager_HowTo : ButtonManager_Menu {

	// Use this for initialization
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

	public void onBtnBattleClick(){
		bool flag = menuMgr.openUI (3);
	}

	public void onBtnStartPlayingClick(){
		bool flag = menuMgr.openUI (4);
	}
	

}
