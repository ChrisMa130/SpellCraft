using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;

namespace HoloToolkit.Sharing
{
    public class GameStateManager : Singleton<GameStateManager>
    {

        public GameObject player;
        public GameObject manager;
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

        void start()
        {
            gameStarted = false;

            // Manager need to have tag "Manager"
            if (manager == null)
                manager = GameObject.FindWithTag("Manager");

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

        void update()
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

                    // Player is alive
                    if (player.GetComponent<Player>().alive)
                    {
                        /* Looks like PickUpManager will take care of spawning
                        if (isPrimary())	{
                            PrepareOrbs();
                        }		
                        */

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
                    /*
                    if ( ) { // all other players finished
                        currentState = GameStatus.End;
                    }
                    */
                    break;

                // All players are finished. Prompt for restart.
                case GameStatus.End:
                    restartButton.SetActive(true);
                    break;
            }
        }

        public bool isPrimary()
        {
            return SharingStage.Instance.ClientRole == ClientRole.Primary;
        }

        // call spawning method in OrbManager
        void PrepareOrbs()
        {
            if (isPrimary())
            {
                PickUpManager.Instance.enabled = true;
            } else
            {
                PickUpManager.Instance.enabled = false; 
            }
        }

        // call UIManager to switch to In-Game Scene
        void StartGame()
        {

        }
    }
}