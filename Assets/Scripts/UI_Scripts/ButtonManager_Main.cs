using UnityEngine;
using UnityEngine.UI;
using System.Collections;



/**
 * This subclass of ButtonManager_Menu handles
 * the buttons of the first loading page of the main menu
 * */
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

    /**
     * This listener listens to the button Play
     * and opens the corresponding Canvas
     * */
	private void onBtnPlayClick()
	{
		bool flag = menuMgr.openUI (6);
	}
    /**
     * This listener listens to the button Spellbook
     * and opens the corresponding Canvas
     * */
    private void onBtnSpellbookClick()
    {
		bool flag = menuMgr.openUI(1);
        
    }

    /**
     * This listener listens to the button How to Play
     * and opens the corresponding Canvas
     * */
    private void onBtnHowToPlayClick()
	{
		bool flag = menuMgr.openUI (2);
	}

    /**
     * This listener listens to the button Rescan
     * and opens the corresponding Canvas
     * */
    private void onBtnRescanClick()
	{
		bool flag = menuMgr.openUI (5);
	}
		
    }
