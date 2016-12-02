using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds and updates the head transforms of remote users.  
/// Head transforms are sent and received in the local coordinate space of the GameObject
/// this component is on.  
/// </summary>
public class RemotePlayerManager : Singleton<RemotePlayerManager>
{
    public class RemoteHeadInfo
    {
        public long UserID;
        public GameObject HeadObject;
        public Vector3 headObjectPositionOffset;
        public int playerHealth;
        public bool Active;
        public bool Anchored;
    }

    public bool alive;
    public GameObject HealthIcon;
    /// <summary>
    /// Keep a list of the remote heads
    /// </summary>
    Dictionary<long, RemoteHeadInfo> remoteHeads = new Dictionary<long, RemoteHeadInfo>();

    public IEnumerable<RemoteHeadInfo> remoteHeadInfos
    {
        get
        {
            return remoteHeads.Values;
        }
    }

    CustomMessages customMessages;

    void Start()
    {
        customMessages = CustomMessages.Instance;
        customMessages.MessageHandlers[CustomMessages.TestMessageID.HeadTransform] = this.UpdateHeadTransform;
        customMessages.MessageHandlers[CustomMessages.TestMessageID.PlayerHealth] = this.UpdatePlayerHealth;
        SharingSessionTracker.Instance.SessionJoined += Instance_SessionJoined;
        SharingSessionTracker.Instance.SessionLeft += Instance_SessionLeft;
        alive = true;
    }

    /// <summary>
    /// Called when a new user is leaving.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Instance_SessionLeft(object sender, SharingSessionTracker.SessionLeftEventArgs e)
    {
        if (remoteHeads.ContainsKey(e.exitingUserId))
        {
            RemoveRemoteHead(this.remoteHeads[e.exitingUserId].HeadObject);
            this.remoteHeads.Remove(e.exitingUserId);
        }
    }

    /// <summary>
    /// Called when a user is joining.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Instance_SessionJoined(object sender, SharingSessionTracker.SessionJoinedEventArgs e)
    {
        if (e.joiningUser.GetID() != SharingStage.Instance.Manager.GetLocalUser().GetID())
        {
            GetRemoteHeadInfo(e.joiningUser.GetID());
        }
    }

    /// <summary>
    /// Gets the data structure for the remote users' head position.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public RemoteHeadInfo GetRemoteHeadInfo(long userID)
    {
        RemoteHeadInfo headInfo;

        // Get the head info if its already in the list, otherwise add it
        if (!this.remoteHeads.TryGetValue(userID, out headInfo))
        {
            headInfo = new RemoteHeadInfo();
            headInfo.UserID = userID;

            headInfo.HeadObject = Instantiate(HealthIcon);
            HealthDisplayBehavior remote = headInfo.HeadObject.transform.FindChild("Sprite").GetComponent<HealthDisplayBehavior>();
            remote.setHealth(headInfo.playerHealth);
            headInfo.headObjectPositionOffset = headInfo.HeadObject.transform.localPosition;
            headInfo.HeadObject.transform.parent = this.transform;
            headInfo.Active = true;

            this.remoteHeads.Add(userID, headInfo);
        }
        return headInfo;
    }

    /// <summary>
    /// Called when a remote user sends a head transform.
    /// </summary>
    /// <param name="msg"></param>
    void UpdateHeadTransform(NetworkInMessage msg)
    {
        // Parse the message
        long userID = msg.ReadInt64();

        Vector3 headPos = customMessages.ReadVector3(msg);

        Quaternion headRot = customMessages.ReadQuaternion(msg);

        // If head info does not exist for this userID, a new one will be created
        // with the call to GetRemoteHeadInfo.
        RemoteHeadInfo headInfo = GetRemoteHeadInfo(userID);
        
        if (headInfo.HeadObject != null)
        {
            // If we don't have our anchor established, don't draw the remote head.
            headInfo.HeadObject.SetActive(headInfo.Anchored);

            headInfo.HeadObject.transform.localPosition = headPos;

            //headInfo.HeadObject.transform.localRotation = headRot;

            /* Our code for testing */
            Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
            Vector3 remoteHealthDisplayPos = anchor.TransformPoint(headPos);

            GameObject healthDisplay = headInfo.HeadObject;
            healthDisplay.transform.parent = this.transform;
            healthDisplay.transform.position = remoteHealthDisplayPos;

        }

        headInfo.Anchored = (msg.ReadByte() > 0);
    }

    /*
     * Updates other players' health in the game world.
     */
    void UpdatePlayerHealth(NetworkInMessage msg)
    {
        // Parse the message
        long userID = msg.ReadInt64();

        RemoteHeadInfo headInfo = GetRemoteHeadInfo(userID);

        headInfo.playerHealth = msg.ReadInt32();
        // Configure the remote user's head sprite
        HealthDisplayBehavior remote = headInfo.HeadObject.transform.FindChild("Sprite").GetComponent<HealthDisplayBehavior>();
        remote.setHealth(headInfo.playerHealth);
        if (headInfo.playerHealth <= 0) {
            alive = false;
        }
    }

    /// <summary>
    /// When a user has left the session this will cleanup their
    /// head data.
    /// </summary>
    /// <param name="remoteHeadObject"></param>
	void RemoveRemoteHead(GameObject remoteHeadObject)
    {
        DestroyImmediate(remoteHeadObject);
    }
}