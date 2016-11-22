/** The GameStateManager Script handles each of the five game states: 
            'start', 'waiting', 'playing', 'loses', and 'end'. 
     Game states for different players are updated simultaneouly with CustomMessages as 
     game progress. When a game state changes, the manager will enable UIManager to handle
     the switching of scenes and UI for the game.
 **/

using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using System;

namespace HoloToolkit.Sharing
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        private int numPlayerAlive;

        // define the five possible game states
        public enum GameStatus
        {
            Start,
            Waiting,
            Playing,
            Loses,
            End
        }

        public GameStatus currentState = GameStatus.Start;

        private Session currentSession;

        public bool sharingServiceReady = false;

        void Awake()
        {
            SetSettings();
            SharingStage.Instance.enabled = true;
        }

        void Start()
        {
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.AnchorRequest] = this.ProcessAnchorRequest;
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.AnchorComplete] = this.ProcessAnchorComplete;
            SharingSessionTracker.Instance.SessionJoined += Instance_SessionJoined;
        }

        private void SetSettings()
        {
            GameObject settings = GameObject.FindWithTag("GameSettings");
            if (settings != null)
            {
                SharingStage.Instance.ServerAddress = settings.GetComponent<GameSettings>().IPAddress;

            }
            else
                Debug.Log("Could not find GameSettings");
        }

        private void Instance_SessionJoined(object sender, SharingSessionTracker.SessionJoinedEventArgs e)
        {
            // We don't need to get this event anymore.
            SharingSessionTracker.Instance.SessionJoined -= Instance_SessionJoined;

            // We still wait to wait a few seconds for everything to settle.
            Invoke("MarkSharingServiceReady", 5);
        }

        private void MarkSharingServiceReady()
        {
            sharingServiceReady = true;
            //ImportExportAnchorManager.Instance.sharingServiceReady = true;
            currentSession = SharingStage.Instance.Manager.GetSessionManager().GetCurrentSession();
        }

        private void SetRole()
        {
            if (currentSession.GetUserCount() == 1)
            {
                Debug.Log("We're primary");
                SharingStage.Instance.ClientRole = ClientRole.Primary;
            }
            else
            {
                Debug.Log("We're secondary");
                SharingStage.Instance.ClientRole = ClientRole.Secondary;
            }
        }

        private void InitAnchor()
        {
            if (SharingStage.Instance.ClientRole == ClientRole.Primary)
            {
                ImportExportAnchorManager.Instance.anchor_ready = true;
            }
        }

        void Update() {
            switch (currentState)
            {
                // Start: set the correct role for players.
                // Only SetRole when we are connected to the Server.
                case GameStatus.Start:
                    if (sharingServiceReady)
                    {
                        // check if local player is the only player, if so the local player 
                        // becomes the Primary Plyaer, 
                        // else Secondary.
                        SetRole();
                        currentState = GameStatus.Waiting;
                    }
                break;
                case GameStatus.Waiting:
                    // if local player is a primary player, 
                    // then initialize the Anchor, and upload it.
                    if (SharingStage.Instance.ClientRole == ClientRole.Primary)
                    {
                        InitAnchor();
                    }
                    // if local player is a secondary player, then keep sending requests for an anchor
                    // until recives one.
                    else if (SharingStage.Instance.ClientRole == ClientRole.Secondary && !ImportExportAnchorManager.Instance.anchor_ready) {
                        CustomMessages.Instance.SendAnchorRequest();
                    }

                    // TO CHANGE: WAIT FOR EVERYONE TO READY BEFORE PLAYING
                    if (ImportExportAnchorManager.Instance.AnchorEstablished)
                    {
                        currentState = GameStatus.Playing;
                    }
                    break;
                /* handles play logics : 
                    1. Turn on the orbManager for The Primary Player.
                    2. Checks if local player is dead, if so, call the proper method WITHIN 
                       the player to model to handle death logic.
                    3. Constantly (or not) check if the Primary player leaves or not, then change the role accordingly.
                 if Primary player:
                */
                case GameStatus.Playing:
                
                    break;

                case GameStatus.Loses:
                    break;
                    
                case GameStatus.End:
                
                    break;

            }
        }

        private void ProcessAnchorRequest (NetworkInMessage msg)
        {
            long userID = msg.ReadInt64();
            if (SharingStage.Instance.ClientRole == ClientRole.Primary)
            {
                if (ImportExportAnchorManager.Instance.AnchorEstablished)
                    CustomMessages.Instance.SendAnchorComplete();
            }
        }

        private void ProcessAnchorComplete (NetworkInMessage msg)
        {
            Debug.Log("start");
            long userID = msg.ReadInt64();
            if (SharingStage.Instance.ClientRole == ClientRole.Secondary)
            {
                Debug.Log("Called process anchorcomplete"); 
                ImportExportAnchorManager.Instance.anchor_ready = true;
            }
        }
    }
}
