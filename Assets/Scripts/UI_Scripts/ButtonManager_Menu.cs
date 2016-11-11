using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * This abstract class represents a button manager in the main menu
 * This is how all button managers are supposed to act initially:
 * find the UIManager, especially the UIManager for the main menu
 * and store the buttons contained in the opened canvas in an array
 * 
 * it also offers its subclasses a listener to the button back
 * which most canvases have
 * */
public abstract class ButtonManager_Menu : MonoBehaviour {

	protected GameObject uiMgr;
	protected MainMenu_UIManager menuMgr;
	protected Component[] components;


	// Use this for initialization
	public void Start ()
	{
		uiMgr = GameObject.FindWithTag("UIManager");

		menuMgr = uiMgr.GetComponent<MainMenu_UIManager> ();
		components = gameObject.GetComponentsInChildren<Button>();
	}

	protected void onBtnBackClick()
	{
		bool flag = menuMgr.back ();
	}


		
}
