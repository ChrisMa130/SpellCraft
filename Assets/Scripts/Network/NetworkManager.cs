using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private bool isSinglePlayer = false;
	private bool gameStarted = false;
	private int IPPlayer1;
	private int IPPlayer2; // should be the same as player1
	private float timer1 = 0;
	private float timer2 = 0;
	private float playTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void requestLocation(){
		
	}

	void timeOut(){

	}

	void gameOver(){

	}
}
