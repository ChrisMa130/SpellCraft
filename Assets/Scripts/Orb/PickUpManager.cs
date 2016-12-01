using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;

/// <summary>
/// This class has creates Orbs on a timer basis. And notifies
/// other players of the creation and desctruction of Orbs.
/// </summary>
public class PickUpManager : Singleton<PickUpManager>
{

    // index works as in Id for the Orb and it just means its index in Orbs
    // nextIndex is simply the next Id available to be issued
    public List<GameObject> Orbs;
    public int nextIndex;
    public GameObject sampleOrb;

    private bool isPrimary;

    private const float RADIUS = 2.5f;

    private const float SPAWN_TIME = 2.0f; // 2 seconds
    private static float tillOrbSpawnTime;

    // Use this for initialization
    // Estabilishes listeners for network messages SendOrb and OrbPickedUp
    void Start()
    {
        tillOrbSpawnTime = SPAWN_TIME;
        Orbs = new List<GameObject>();
        nextIndex = 0;
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.SendOrb] = this.ProcessRemoteOrb;
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.OrbPickedUp] = this.RemoveRemoteOrb;
    }

    // Update is called once per frame
    // If player is primary, then spawn orb on a timer.
    void Update()
    {
        // check if game started before generating orbs
        isPrimary = (SharingStage.Instance.ClientRole == ClientRole.Primary);
        if (isPrimary)
        {
            tillOrbSpawnTime -= Time.deltaTime;
            if (tillOrbSpawnTime <= 0)
            {
                Vector3 orbLocation = calculateOrbLocation();
                // broadcast orb to others
                GenerateOrb(orbLocation, nextIndex);
                Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
                CustomMessages.Instance.SendOrb(anchor.InverseTransformPoint(orbLocation), nextIndex);
                nextIndex++;
                //create orb for primary
                tillOrbSpawnTime = SPAWN_TIME;
            }
        }

    }

    // Returns a random Vector3 (to be used to populate an Orb
    // The vector will be at the height of the Camera and
    //      somewhere within a RADIUS meter circle around the anchor on that plane
    private Vector3 calculateOrbLocation()
    {

        Vector3 r = new Vector3(Random.Range(-RADIUS, RADIUS), Camera.main.transform.position.y - 0.5f,
            Random.Range(-RADIUS, RADIUS));
        return r;
    }

    // Parameter-msg: a Network message corresponding to a SendOrb type message
    // Creates the Orb specified in the message
    public void ProcessRemoteOrb(NetworkInMessage msg)
    {
        // userID not used for now
        long userID = msg.ReadInt64();
        Vector3 remoteOrbPosition = CustomMessages.Instance.ReadVector3(msg);
        int remoteIndex = msg.ReadInt32();

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        GenerateOrb(anchor.TransformPoint(remoteOrbPosition), remoteIndex);
    }

    // Parameter-msg: a Network message correspoding to a OrbPickedUp type message
    // Deletes the Orb picked up the other player
    public void RemoveRemoteOrb(NetworkInMessage msg)
    {
        // userID not used for now
        long userID = msg.ReadInt64();
        int remoteIndex = msg.ReadInt32();
        RemoveOrb(remoteIndex);
        
    }

    // Parameter-index: the id of the index created
    // Destroys the Orb specified by the index without increasing magic
    public void RemoveOrb(int index)
    {
        Destroy(Orbs[index]);
        Orbs[index] = null;
        Debug.Log("Pressed");
    }

    // Parameter-loc: the location to put the Orb
    //          -index: the id of the orb
    // Creates an Orb
    public void GenerateOrb(Vector3 loc, int index)
    {
        GameObject spawnedOrb = Instantiate(sampleOrb, loc, Quaternion.identity) as GameObject;
        spawnedOrb.transform.parent = this.transform;
        spawnedOrb.GetComponent<MeshRenderer>().enabled = true;
        spawnedOrb.GetComponent<Orb>().SetIndex(index);
        Orbs.Add(spawnedOrb);
    }
}