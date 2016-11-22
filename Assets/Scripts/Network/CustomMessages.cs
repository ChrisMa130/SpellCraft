using System;
using System.Net;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class has two purposes. The first purpose is that it contains functions
/// that lets other classes broadcast out network messages to clients in our system.
/// The second purpose is that it provides a MessageHandler dictionary. This dictionary
/// allows these classes to register their own functions with a certain MessageID.
/// When the network has an in-message, it will automatically call back these functions.
/// </summary>
public class CustomMessages : Singleton<CustomMessages> {

    /* MessageType (first byte of message sent/received: describes that kind of message we have).
     * Current messages includes
     * 1. SpellMessage: Indicates a spell being casted 
     * 2. PlayerHealth: Health of player.
     * 3. HeadTransform: Location of player's head.
     * 4. StageTransform: Related to transferring anchors.
     * 5. SendOrb: When the Primary Player spawns an orb.
     * 6. PlayerHit: When a player is hit by a spell.
     * 7. OrbPickedUp: When a player picks up an orb.
     * 8. PlayerStatus: Not used at the moment.
     * 9. DeathMessage: When a player dies.
     * 10. AnchorRequest: When a secondary client requests to download an anchor.
     * 11. AnchorComplete: When the primary client is ready to send an anchor.
     * 12. Max: Just a placeholder to mark end of message types. Ignore.
     */
    public enum TestMessageID : byte
	{
		SpellMessage = MessageID.UserMessageIDStart,
        PlayerHealth,
        HeadTransform,
        StageTransform,
        SendOrb,
        PlayerHit,
        OrbPickedUp,
        PlayerStatus,
        DeathMessage,
        AnchorRequest,
        AnchorComplete,
        Max
    }

    /// <summary>
    /// Retrieve ClientRole of the current user.
    /// </summary>
    /// <returns>ClientRole.Primary or ClientRole.Secondary</returns>
    public ClientRole isPrimary()
    {
        return SharingStage.Instance.ClientRole;
    }

    // Unused at the moment.
    public enum UserMessageChannels
    {
        Anchors = MessageChannel.UserMessageChannelStart,
    }

    /// <summary>
    /// Cache the local user's ID to use when sending messages
    /// </summary>
    public long localUserID
    {
        get; set;
    }


    /*
     * Below contains the Dictionary that lets other classes register their functions with this class
     * so that their functions will be called back automatically when there is a message from the network.
     * Users will have to add to the MessageHandlers dictionary via the following syntax:
     * CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.USER_MESSAGE_ID] = this.USER_FUNCTION;
     * Where USER_MESSAGE_ID refers to what message they want to register with, and USER_FUNCTION referring to the
     * function in their class that they want called back.
     */
    public delegate void MessageCallback(NetworkInMessage msg);
    private Dictionary<TestMessageID, MessageCallback> _MessageHandlers = new Dictionary<TestMessageID, MessageCallback>();
    public Dictionary<TestMessageID, MessageCallback> MessageHandlers
    {
        get
        {
            return _MessageHandlers;
        }
    }

    /// <summary>
    /// Helper object that we use to route incoming message callbacks to the member
    /// functions of this class
    /// </summary>
    NetworkConnectionAdapter connectionAdapter;

    /// <summary>
    /// Cache the connection object for the sharing service
    /// </summary>
    NetworkConnection serverConnection;

    // Called initially. Will register the SharingManagerConnected function below
    // to be called when SharingStage prefab detects that we have connected.
    void Start() {
        SharingStage.Instance.SharingManagerConnected += SharingManagerConnected;
    }

    // Update is called once per frame
    void Update()
    {
        // Unused at the moment
    }

    /// <summary>
    /// Returns whether we have the lowest user id.
    /// Unused at the moment.
    /// </summary>
    /// <returns>true, if the user has the lowest user id</returns>
    bool LocalUserHasLowestUserId()
    {
        long localUserId = CustomMessages.Instance.localUserID;
        foreach (long userid in SharingSessionTracker.Instance.UserIds)
        {
            if (userid < localUserId)
            {
                return false;
            }
        }

        return true;
    }

    // Called when connected to a sharing service.
    private void SharingManagerConnected(object sender, System.EventArgs e)
    {
        InitializeMessageHandlers();
    }

    /// <summary>
    /// Sets up our dictionary and registers various callbacks.
    /// </summary>
    void InitializeMessageHandlers()
    {
        SharingStage sharingStage = SharingStage.Instance;
        if (sharingStage != null)
        {
            serverConnection = sharingStage.Manager.GetServerConnection();
            connectionAdapter = new NetworkConnectionAdapter();
        }
        // Register our OnMessageReceived function below with connection adapter,
        // so we can dispatch messages received on the network to classes that register
        // with our dictionary.
        connectionAdapter.MessageReceivedCallback += OnMessageReceived;

        // Cache the local user ID
        this.localUserID = SharingStage.Instance.Manager.GetLocalUser().GetID();

        for (byte index = (byte)TestMessageID.SpellMessage; index < (byte)TestMessageID.Max; index++)
        {
            if (MessageHandlers.ContainsKey((TestMessageID)index) == false)
            {
                // Just set up the dictionary.
                MessageHandlers.Add((TestMessageID)index, null);
            }
            // Add listeners for each possible message with its index.
            serverConnection.AddListener(index, connectionAdapter);
        }
    }

    // Helper function. Just creates a message with type and id.
    private NetworkOutMessage CreateMessage(byte MessageType)
    {
        NetworkOutMessage msg = serverConnection.CreateMessage(MessageType);
        msg.Write(MessageType);
        // Add the local userID so that the remote clients know whose message they are receiving
        msg.Write(localUserID);
        return msg;
    }

    // Below contains functions for broadcasting out information related to our game.
    // All vectors and directions should be relative to the anchor that we have established. Users are
    // responsible for this.

    /// <summary>
    /// Broadcasts out that we have casted a spell of a certain spellIndex type, at the specified position and direction.
    /// </summary>
    /// <param name="position">Position of the spell</param>
    /// <param name="direction">Direction of the spell</param>
    /// <param name="spellIndex">Index into the GameObject arrays of spells. Used to indentify which spell was casted</param>
    public void SendSpell(Vector3 position, Vector3 direction, int spellIndex)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.SpellMessage);

            // Want to send: position, direction, and spell index in our message
            AppendVector3(msg, position);
            AppendVector3(msg, direction);
            msg.Write(spellIndex);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out that we have picked up an orb, indentified by the orb index
    /// </summary>
    /// <param name="index">Index of the orb that we have picked up</param>
    public void SendOrbPickedUpMessage(int index)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.OrbPickedUp);

            msg.Write(index);
            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out that we request a response if the primary player is ready to export their anchor.
    /// </summary>
    public void SendAnchorRequest()
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.AnchorRequest);
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.UnreliableSequenced,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out if primary player is ready to export their anchor.
    /// </summary>
    public void SendAnchorComplete()
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.AnchorComplete);
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out that we have died.
    /// </summary>
    public void SendDeathMessage()
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.DeathMessage);
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out that we have spawned an orb at a certain position with the index identifying the orb.
    /// Should only be called by the user with the "Primary" client role.
    /// </summary>
    /// <param name="position">Position of the orb spawned</param>
    /// <param name="index">Index of the spawned orb</param>
    public void SendOrb(Vector3 position, int index)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.SendOrb);

            AppendVector3(msg, position);
            msg.Write(index);
            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out our head position and rotation to other users.
    /// </summary>
    /// <param name="position">Position of the user's head</param>
    /// <param name="rotation">Rotation of the user's head</param>
    /// <param name="HasAnchor">unused, user should send as 0x1</param>
    public void SendHeadTransform(Vector3 position, Quaternion rotation, byte HasAnchor)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.HeadTransform);

            AppendTransform(msg, position, rotation);

            msg.Write(HasAnchor);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.UnreliableSequenced,
                MessageChannel.Avatar);
        }
    }

    /// <summary>
    /// Broadcasts out the health of the user.
    /// </summary>
    /// <param name="playerHealth">The health of the user</param>
    public void UpdatePlayerHealth(int playerHealth)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.PlayerHealth);
            msg.Write(playerHealth);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.UnreliableSequenced,
                MessageChannel.Avatar);
        }
    }


    // Unused at the moment, but basically if we do not wish to continue receiving messages.
    void OnDestroy()
    {
        if (this.serverConnection != null)
        {
            for (byte index = (byte)TestMessageID.SpellMessage; index < (byte)TestMessageID.Max; index++)
            {
                this.serverConnection.RemoveListener(index, this.connectionAdapter);
            }
            this.connectionAdapter.MessageReceivedCallback -= OnMessageReceived;
        }
    }

    /// <summary>
    /// Called whenever we receive a message from the network connection.
    /// </summary>
    /// <param name="connection">Current connection</param>
    /// <param name="msg">Message that we have received</param>
    void OnMessageReceived(NetworkConnection connection, NetworkInMessage msg)
    {
        // Read the message type.
        byte messageType = msg.ReadByte();
        // Get the function that the user has registered to this message type.
        MessageCallback messageHandler = MessageHandlers[(TestMessageID)messageType];

        if ((TestMessageID)messageType == TestMessageID.StageTransform)
            Debug.Log("AnchorReceived");

        if (messageHandler != null)
        {
            // Call function corresponding to message type.
            messageHandler(msg);
        }
    }

    // Helper function: just write a vector3 and rotation to a message
    void AppendTransform(NetworkOutMessage msg, Vector3 position, Quaternion rotation)
    {
        AppendVector3(msg, position);
        AppendQuaternion(msg, rotation);
    }

    // Helper function: write a vector3 to a message
    void AppendVector3(NetworkOutMessage msg, Vector3 vector)
    {
        msg.Write(vector.x);
        msg.Write(vector.y);
        msg.Write(vector.z);
    }

   // Helper function: write a rotation to a message.
    void AppendQuaternion(NetworkOutMessage msg, Quaternion rotation)
    {
        msg.Write(rotation.x);
        msg.Write(rotation.y);
        msg.Write(rotation.z);
        msg.Write(rotation.w);
    }

    /// <summary>
    /// Reads a vector3 from a network message.
    /// </summary>
    /// <param name="msg">Message to read from</param>
    /// <returns>A vector3 that corresponds to the next 12 bytes</returns>
    public Vector3 ReadVector3(NetworkInMessage msg)
    {
        return new Vector3(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }

    /// <summary>
    /// Read a Quaternion from a network message.
    /// </summary>
    /// <param name="msg">Message to read from</param>
    /// <returns>A Quaternion, corresponding to the next 16 bytes</returns>
    public Quaternion ReadQuaternion(NetworkInMessage msg)
    {
        return new Quaternion(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }
}
