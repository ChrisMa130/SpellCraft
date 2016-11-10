using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/**
 * This subclass of ButtonManager_Menu handles the listeners 
 * of the buttons in the page "Play"
 * */
public class ButtonManager_Lobby : ButtonManager_Menu {


	InputField textField; 
	int count;
	bool error;
	string text;
	byte dots;
	byte digitsInBlock;

	void Start () {
		base.Start ();

		textField = GameObject.FindWithTag("IP_Field").GetComponent<InputField>();
		text = "";
		dots = 0;
		digitsInBlock = 0;

		foreach (Button btn in components)
		{ 

			switch (btn.name)
			{
			case "Ready": btn.onClick.AddListener(onBtnReadyClick);  break;
			case "Back": btn.onClick.AddListener(onBtnBackClick);  break;
			case "Btn1": btn.onClick.AddListener(onBtn1Click);  break;
			case "Btn2": btn.onClick.AddListener(onBtn2Click);  break;
			case "Btn3": btn.onClick.AddListener(onBtn3Click);  break;
			case "Btn4": btn.onClick.AddListener(onBtn4Click);  break;
			case "Btn5": btn.onClick.AddListener(onBtn5Click);  break;
			case "Btn6": btn.onClick.AddListener(onBtn6Click);  break;
			case "Btn7": btn.onClick.AddListener(onBtn7Click);  break;
			case "Btn8": btn.onClick.AddListener(onBtn8Click);  break;
			case "Btn9": btn.onClick.AddListener(onBtn9Click);  break;
			case "Btn0": btn.onClick.AddListener(onBtn0Click);  break;
			case "BtnDot": btn.onClick.AddListener(onBtnDotClick);  break;
			case "BtnBS": btn.onClick.AddListener(onBtnBSClick);  break;
				
				
			default: break;
			}
		}
	}

	private void addDigit(char i){

		if (error) {
			clear ();
			error = false;
		
		}

		if (dots < 4) {
		
			if (i == '.') {
				if (dots < 3) {
					text += '.';
					digitsInBlock = 0;
					dots++;
				}

			} else {
				if (digitsInBlock == 3) {

					if (dots < 3) {
						dots++;
						digitsInBlock = 0;
						text += '.';
					} else {
						return;
					}
				}

				text += i;
				digitsInBlock++;
			}

			textField.text = text;
		
		}



	
	}

	private void erase(){
		if (dots > 0 || digitsInBlock > 0) {

			if (text[text.Length - 1] != '.') {
			
				text = text.Remove (text.Length - 1);
				digitsInBlock--;

			} else {
				text = text.Remove (text.Length - 1);
				dots--;
				digitsInBlock = countTillDot (text);

			}


			textField.text = text;
		
		}
	}

	private void clear(){
		text = "";
		dots = 0;
		digitsInBlock = 0;

		textField.text = text;
	
	}
	private byte countTillDot(string s){
		int iter = s.Length - 1;
		byte count = 0;
		while (iter >= 0 && s [iter] != '.') {
			iter--;
			count++;
		}

		return count;
		
	}

	public void onBtnReadyClick()
	{
		if (dots < 3 || !menuMgr.set_IP (text)) {
			clear ();
			textField.text = "Invalid IP";
			error = true;
		} else {
			SceneManager.LoadScene ("Prototype_Leo");
		}

	}

	public void onBtn1Click()
	{
		addDigit ('1');
	}
	public void onBtn2Click()
	{	
		addDigit ('2');
	}
	public void onBtn3Click()
	{
		addDigit ('3');

	}
	public void onBtn4Click()
	{
		addDigit ('4');

	}
	public void onBtn5Click()
	{
		addDigit ('5');
	}
	public void onBtn6Click()
	{
		addDigit ('6');
	}
	public void onBtn7Click()
	{
		addDigit ('7');

	}
	public void onBtn8Click()
	{
		addDigit ('8');

	}
	public void onBtn9Click()
	{
		addDigit ('9');

	}
	public void onBtn0Click()
	{
		addDigit ('0');

	}
	public void onBtnDotClick()
	{
		addDigit ('.');

	}
	public void onBtnBSClick()
	{
		erase ();
	}




}
