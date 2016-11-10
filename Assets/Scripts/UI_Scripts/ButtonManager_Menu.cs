using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ButtonManager_Menu : MonoBehaviour {

	public GameObject uiMgr;
	public MainMenu_UIManager menuMgr;
	public Component[] components;


	// Use this for initialization
	public void Start ()
	{
		uiMgr = GameObject.FindWithTag("UIManager");

		menuMgr = uiMgr.GetComponent<MainMenu_UIManager> ();
		components = gameObject.GetComponentsInChildren<Button>();
	}

	public void onBtnBackClick()
	{
		bool flag = menuMgr.back ();
	}


		
}
