using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;

namespace HoloToolkit.Sharing
{
    public class GameStateManager : Singleton<GameStateManager>
    {

        public GameObject player;
        public GameObject Anchor;
        public bool gameStarted;

        // define possible game states
        private enum GameStatus
        {
            Start,
            Waiting,
            Playing,
            Loses,
            End
        }

        private GameStatus currentState = GameStatus.Start;

        // store corresponding Scenes/UI for each game state
        public GameObject waitingCanvas;
        public GameObject inGameCanvas;
        public GameObject winnerCanvas;
        public GameObject loserCanvas;
        public GameObject restartButton;

        void Start()
        {
            Anchor.GetComponent<PickUpManager>().enabled = false;
            Anchor.GetComponent<SpellManager>().enabled = false;

            gameStarted = false;

            // Manager need to have tag "Manager"
            if (Anchor == null)
                Anchor = GameObject.FindWithTag("Anchor");

            // Player is the "Main Camera"
            if (player == null)
                player = GameObject.FindWithTag("MainCamera");

            // set game-over canvases as inactive
            waitingCanvas.SetActive(true);
            inGameCanvas.SetActive(false);
            winnerCanvas.SetActive(false);
            loserCanvas.SetActive(false);
            restartButton.SetActive(false);
        }

        void Update()
        {
            switch (currentState)
            {
                // Start: do nothing
                case GameStatus.Start:
                    break;

                /** Waiting: waiting for one of more players to connect.
                 ***** player should click "Start" button to switch state to playing
                 ***** need a seperate button script
                 */
                case GameStatus.Waiting:
                    Debug.Log("State: Waiting");
                    if (gameStarted)
                    {
                        waitingCanvas.SetActive(false);
                        inGameCanvas.SetActive(true);
                    }
                    break;

                /** Playing: game started. While player is alive, simultaneously 
                    check on all players' life status. If player is not alive,
                    swtich game state to Loses. If all other player died ahead of 
                    time, then this player won.
                 **/
                case GameStatus.Playing:
                    Debug.Log("State: Playing");

                    // Activate PickUpManager to enable spawning of orbs
                    Anchor.GetComponent<PickUpManager>().enabled = true;
                    Anchor.GetComponent<SpellManager>().enabled = true;

                    

                    // Player is alive
                    if (player.GetComponent<Player>().alive)
                    {
                        /*
                        // all other player died
                        if () {
                            inGameCanvas.SetActive(false);
                            winnerCanvas.SetActive(true);
                            currentState = GameStatus.End;
                        }
                        */
                    }

                    // Player is dead
                    else
                    {
                        inGameCanvas.SetActive(false);
                        loserCanvas.SetActive(true);
                        CustomMessages.Instance.SendDeathMessage();
                        currentState = GameStatus.Loses;
                    }
                    break;

                // Player lost and waiting for other players to finish.
                case GameStatus.Loses:
                    Debug.Log("State: Loses");
                    /*
                    if ( ) { // all other players finished
                        currentState = GameStatus.End;
                    }
                    */
                    break;

                // All players are finished. Prompt for restart.
                case GameStatus.End:
                    Debug.Log("State: End");
                    restartButton.SetActive(true);
                    break;
            }
        }
    }
}