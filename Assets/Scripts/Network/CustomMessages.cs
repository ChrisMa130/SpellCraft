using System;
using System.Net;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles networking for users.
/// </summary>
public class CustomMessages : Singleton<CustomMessages> {

    // TIMEOUT default value = 5000 milliseconds
    private const long TIMEOUT = 5000;

    private bool isSinglePlayer = false;
    private bool gameStarted = false;
    private IPAddress IPPlayer1 = null;
    private IPAddress IPPlayer2 = null; // should be the same as player1
    private long timer1 = 0; // timeouts, are they needed?
    private long timer2 = 0;
    private float playTimer = 0;

    // Timing stuff:
    private static long ctime = 0; // ctime will represent the current time.
    private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    // For timeouts: just calculate time in milliseconds.
    private static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }


    // MessageType (first byte of message received: describes that kind of message we have).
    public enum TestMessageID : byte
	{
		HeadTransform = MessageID.UserMessageIDStart,
		SpellMessage,
		LocationMessage,
        PlayerHealth,
        SendHeadTransform,
        StageTransform,
        SendOrb, // or spawned up
        PlayerHit,
        OrbPickedUp,
        PlayerStatus,
        DeathMessage,
        /*
        Stuff like: PlayerHit, OrbPickedUp, PlayerStatus(send's hp and mana?) etc..          
          
         
         */
        Max
    }

    public ClientRole isPrimary()
    {
        return SharingStage.Instance.ClientRole;
    }

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


    /// <summary>
    /// FOR SHARING LOCATION: EITHER CALL IN UPDATE OF HEADLOCATION OR
    /// HERE IN UPDATE FOR REQUEST LOCATION.
    /// </summary>

    // The SharingStage ? 
    //private static SharingStage ss = SharingStage.Instance;
    //private static NetworkConnection network = ss.Manager.GetServerConnection();

    /// <summary>
    /// See projectilelauncher.cs in academy 240 start function.
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MessageCallback(NetworkInMessage msg);
    private Dictionary<TestMessageID, MessageCallback> _MessageHandlers = new Dictionary<TestMessageID, MessageCallback>();
    public Dictionary<TestMessageID, MessageCallback> MessageHandlers
    {
        get
        {
            return _MessageHandlers;
        }
    }

    public void addCallBack(TestMessageID mid, MessageCallback callback)
    {
        _MessageHandlers[mid] = callback;
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

    // Use this for initialization
    void Start() {
        SharingStage.Instance.SharingManagerConnected += SharingManagerConnected;
        ctime = CurrentTimeMillis();
        timer1 = ctime += TIMEOUT;
        //SharingStage.Instance.
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalUserHasLowestUserId())
        {
            SharingStage.Instance.ClientRole = HoloToolkit.Sharing.ClientRole.Primary;
        }
        else
        {
            SharingStage.Instance.ClientRole = HoloToolkit.Sharing.ClientRole.Secondary;
        }

        /*if (ctime == 0)
        {
            // initiliaze ctime, then set timers to be ctime + timeout.
            ctime = CurrentTimeMillis();
            timeoutTimer = ctime += TIMEOUT;
            timeoutTimer = ctime += TIMEOUT;
            return;
        }*/
        // ctime not zero: we check timeout.
        // first update ctime.
        ctime = CurrentTimeMillis();
        if (ctime > timer1)
        {
            // We timed out.
            // Do stuff:
        }
        /* if (ctime > timer2)
         {
             // player two timed out.
             // Do stuff:
         }*/
    }

    // IMPORTANT FUNCTION!!!
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

    private void SharingManagerConnected(object sender, System.EventArgs e)
    {
        InitializeMessageHandlers();
    }

    void InitializeMessageHandlers()
    {
        SharingStage sharingStage = SharingStage.Instance;
        if (sharingStage != null)
        {
            serverConnection = sharingStage.Manager.GetServerConnection();
            connectionAdapter = new NetworkConnectionAdapter();
        }

        connectionAdapter.MessageReceivedCallback += OnMessageReceived;

        // Cache the local user ID
        this.localUserID = SharingStage.Instance.Manager.GetLocalUser().GetID();

        for (byte index = (byte)TestMessageID.HeadTransform; index < (byte)TestMessageID.Max; index++)
        {
            if (MessageHandlers.ContainsKey((TestMessageID)index) == false)
            {
                MessageHandlers.Add((TestMessageID)index, null);
            }

            serverConnection.AddListener(index, connectionAdapter);
        }
    }

    private NetworkOutMessage CreateMessage(byte MessageType)
    {
        NetworkOutMessage msg = serverConnection.CreateMessage(MessageType);
        msg.Write(MessageType);
        // Add the local userID so that the remote clients know whose message they are receiving
        msg.Write(localUserID);
        return msg;
    }

    public void SendSpell(Vector3 position, Vector3 direction)
    {
        // If we are connected to a session, broadcast our head info
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.SpellMessage);

            // HAVE HERE SPELL ID? OR AFTER APPENDTRANSFORM. OR SPELLMESSAGE = incendio, SPELLMESSAGE + 1 = icebeam etc etc.

            AppendVector3(msg, position);
            AppendVector3(msg, direction);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }

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

    public void SendOrb(Vector3 position, int index)
    {
        // If we are connected to a session, broadcast our head info
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.SendOrb);

            // HAVE HERE SPELL ID? OR AFTER APPENDTRANSFORM. OR SPELLMESSAGE = incendio, SPELLMESSAGE + 1 = icebeam etc etc.

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

    public void setSinglePlayer(bool sp)
    {
        this.isSinglePlayer = sp;
    }

    public void setGameStarted(bool gs)
    {
        this.gameStarted = gs;
    }


    /*
	public void resetTimerPlayerOne(){
		timer1 = ctime += TIMEOUT;
	}

	public void resetTimerPlayerTwo(){
		timer2 = ctime += TIMEOUT;
	}*/

    public void requestLocation()
    {

    }

    void timeOut()
    {

    }

    void gameOver()
    {

    }

    public void SendHeadTransform(Vector3 position, Quaternion rotation)
    {
        // If we are connected to a session, broadcast our head info
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.HeadTransform);

            AppendTransform(msg, position, rotation);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.UnreliableSequenced,
                MessageChannel.Avatar);
        }
    }

    public void UpdatePlayerHealth(Vector3 headPosition, int playerHealth)
    {
        if (this.serverConnection != null && this.serverConnection.IsConnected())
        {
            // Create an outgoin network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)TestMessageID.PlayerHealth);
            AppendVector3(msg, headPosition);
            msg.Write(playerHealth);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            this.serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.Reliable,
                MessageChannel.Avatar);
        }
    }


    void OnDestroy()
    {
        if (this.serverConnection != null)
        {
            for (byte index = (byte)TestMessageID.HeadTransform; index < (byte)TestMessageID.Max; index++)
            {
                this.serverConnection.RemoveListener(index, this.connectionAdapter);
            }
            this.connectionAdapter.MessageReceivedCallback -= OnMessageReceived;
        }
    }

    void OnMessageReceived(NetworkConnection connection, NetworkInMessage msg)
    {
        timer1 = ctime += TIMEOUT;

        byte messageType = msg.ReadByte();
        MessageCallback messageHandler = MessageHandlers[(TestMessageID)messageType];

        if ((TestMessageID)messageType == TestMessageID.StageTransform)
            Debug.Log("AnchorReceived");

        if (messageHandler != null)
        {
            messageHandler(msg);
        }
    }

    void AppendTransform(NetworkOutMessage msg, Vector3 position, Quaternion rotation)
    {
        AppendVector3(msg, position);
        AppendQuaternion(msg, rotation);
    }

    void AppendVector3(NetworkOutMessage msg, Vector3 vector)
    {
        msg.Write(vector.x);
        msg.Write(vector.y);
        msg.Write(vector.z);
    }

    void AppendQuaternion(NetworkOutMessage msg, Quaternion rotation)
    {
        msg.Write(rotation.x);
        msg.Write(rotation.y);
        msg.Write(rotation.z);
        msg.Write(rotation.w);
    }

    public Vector3 ReadVector3(NetworkInMessage msg)
    {
        return new Vector3(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }

    public Quaternion ReadQuaternion(NetworkInMessage msg)
    {
        return new Quaternion(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }
}
