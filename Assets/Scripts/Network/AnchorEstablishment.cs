using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;

public class AnchorEstablishment : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // We care about getting updates for the anchor transform.
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.StageTransform] = this.OnStageTransfrom;

        // And when a new user join we will send the anchor transform we have.
        SharingSessionTracker.Instance.SessionJoined += Instance_SessionJoined;
    }

    private void Instance_SessionJoined(object sender, SharingSessionTracker.SessionJoinedEventArgs e)
    {
            CustomMessages.Instance.SendStageTransform(transform.localPosition, transform.localRotation);
    }

    /// <summary>
	/// When a remote system has a transform for us, we'll get it here.
	/// </summary>
	/// <param name="msg"></param>
	void OnStageTransfrom(NetworkInMessage msg)
    {
        // We read the user ID but we don't use it here.
        msg.ReadInt64();
        Debug.Log("Received Anchor");
        Vector3 localPos = CustomMessages.Instance.ReadVector3(msg);
        Debug.Log(localPos);
        transform.localPosition = localPos;
        Quaternion localRot = CustomMessages.Instance.ReadQuaternion(msg);
        transform.localRotation = localRot;
    }
}
