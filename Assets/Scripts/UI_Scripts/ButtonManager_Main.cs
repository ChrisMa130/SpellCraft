using UnityEngine;
using UnityEngine.UI;
using System.Collections;




public class ButtonManager_Main : ButtonManager_Menu
    {


        // Use this for initialization
	void Start()
    {
		base.Start ();
        foreach (Button btn in components)
        {

            switch (btn.name)
            {
                case "Play": btn.onClick.AddListener(onBtnPlayClick);  break;
				case "Spellbook": btn.onClick.AddListener(onBtnSpellbookClick);  break;
				case "HowToPlay": btn.onClick.AddListener(onBtnHowToPlayClick);  break;
				case "Rescan": btn.onClick.AddListener(onBtnRescanClick);  break;
				
                default: break;
            }
        }
    }   

	public void onBtnPlayClick()
	{
		bool flag = menuMgr.openUI (6);
	}

    public void onBtnSpellbookClick()
    {
		bool flag = menuMgr.openUI(1);
        
    }
		
	public void onBtnHowToPlayClick()
	{
		bool flag = menuMgr.openUI (2);
	}

	public void onBtnRescanClick()
	{
		bool flag = menuMgr.openUI (5);
	}
		
    }
