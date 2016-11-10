using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System;


/**
 * This class represents the UI Manager which will 
 * be working when the game is still in the main menu
 * 
 * It defines an inner class UISystem documented below
 * and contains a list of all the possible UISystems that 
 * the Manager could implement
 * and a stack of UISystems which contains all the previously
 * active UISystems that the Manager might go back to and open again
 * */
public class MainMenu_UIManager : MonoBehaviour {


    /**
     * a UISystem basically representents a page in the main Menu
     * the GameObject UIObj will contain the canvas that 
     * should be visible when the page is opened
     * */
    private class UISystem
    {
        public int id;
        public string name;
        public bool lastUI;
        public GameObject UIObj = null;
    }

    private List<UISystem> UISystems;
    private Stack<UISystem> UIStack;
    private UISystem currentUI;
	private int[] IP_address;
	private string IP_String;

    void Start () {
        UISystems = new List<UISystem>();
        UISystems.Add(new UISystem() { id = 0, name = "MainCanvas", lastUI = true });
        UISystems.Add(new UISystem() { id = 1, name = "SpellbookCanvas", lastUI = false});
        UISystems.Add(new UISystem() { id = 2, name = "HowToCanvas", lastUI = false });
        UISystems.Add(new UISystem() { id = 3, name = "BattleCanvas", lastUI = false });
        UISystems.Add(new UISystem() { id = 4, name = "StartPlayingCanvas", lastUI = false });
        UISystems.Add(new UISystem() { id = 5, name = "RescanCanvas", lastUI = false });
		UISystems.Add(new UISystem() { id = 6, name = "LobbyCanvas", lastUI = false });

        UIStack = new Stack<UISystem>();

        openUI(0);
    }

    void Awake()
    {
        checkEventSystem();
        currentUI = null;
		IP_address = new int[4];
		IP_String = "...";
    }


    /**
     * checks if the current scene contains an Event System,
     * if not create one and add it
     * */
    void checkEventSystem()
    {
        var ESObj = GameObject.Find("EventSystem");
        if (ESObj == null)
        {
            ESObj = new GameObject("EventSystem");
            ESObj.AddComponent<EventSystem>();
            ESObj.AddComponent<StandaloneInputModule>();
            ESObj.AddComponent<HoloLensInputModule>();
        }
    }


    /**
     * discard the current UISystem and goes back to the last active one
     * 
     * @returns true if everything goes well
     * */
	public bool back()
	{

		GameObject.DestroyImmediate (currentUI.UIObj);

		currentUI = UIStack.Pop ();
		currentUI.UIObj.SetActive (true);


		return true;

	}

    /**
     * @param id: the id of the UISystem that needs to be opened
     * @returns true if opened successfully or if already opened
     * 
     * pushes the actual UISystem to the stack
     * */
    public bool openUI(int id)
    {
		if (id < 0 || id >= UISystems.Count) { return false; }

        if(currentUI != null && currentUI.id == id) { return true; }

        UISystem ui = UISystems[id];

		CloseCurrentUI();
        

        var o = Resources.Load("Prefabs/Prototype/UI/" + ui.name);

        if(o == null) { return false; }

        var obj = Instantiate(o);
        if(obj == null) { return false; }


        currentUI = ui;
        currentUI.UIObj = (GameObject) obj;

        return true;

    }

    /**
     * closes the current UISystem by setting it 
     * inactive and pushing it to the stack
     * */
    public void CloseCurrentUI()
    {

        if(currentUI != null)
        {
            currentUI.UIObj.SetActive(false);
            UIStack.Push(currentUI);
        }

    }

    /**
     * @returns a deep copy of the current IP address, 
     * which is given in the form of an int array 
     * of length 4 containing the 4 bytes of the IP
     * */
	public int[] get_IP () {
		int[] ip = new int[4];

		for (byte i = 0; i < 4; i++) {
			ip [i] = IP_address [i];
		}

		return ip;
	}

    /**
     * @returns the current IP address in the form of a string
     * */
	public string get_IP_string(){
		return IP_String;
	}


    /**
     * @param s: IP in the form "***.***.***.***" where * can be a digit or empty
     * @returns bool if the IP is valid
     * 
     * sets the current IP to the one represented by s only if it's valid
     * */
	public bool set_IP(string s){
		bool result = true;
		string[] bytes = s.Split ('.');

		for (byte i = 0; i < 4; i++) {
			int parsed;
			if (bytes [i] == "") {
				parsed = 0;
			} else {
				parsed = Int32.Parse (bytes[i]);
			}
			if (0 <= parsed && parsed < 256) {
				IP_address [i] = parsed;
			} else {
				IP_address [i] = 0;
				result = false;
			}
		}
		if (result) {
			IP_String = s;
		}


		return result;
	}

}
