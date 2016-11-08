using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;

public class PickUpManager : Singleton<PickUpManager>
{

    public List<GameObject> Orbs;
    public int nextIndex;
    public GameObject sampleOrb;

    private bool isPrimary;

    private const float SPAWN_TIME = 5.0f; // 5 seconds
    private static float tillOrbSpawnTime;

    // Use this for initialization
    void Start()
    {
        tillOrbSpawnTime = SPAWN_TIME;
        Orbs = new List<GameObject>();
        nextIndex = 0;
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.SendOrb] = this.ProcessRemoteOrb;
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.OrbPickedUp] = this.RemoveRemoteOrb;
    }

    // Update is called once per frame
    void Update()
    {
        // check if game started before generating orbs
        //isPrimary = CustomMessages.
        if (true)
        {
            tillOrbSpawnTime -= Time.deltaTime;
            if (tillOrbSpawnTime <= 0)
            {
                // TODO: implement spawnOrb
                Vector3 orbLocation = calculateOrbLocation();
                // broadcast orb to others
                GenerateOrb(orbLocation, nextIndex);
                CustomMessages.Instance.SendOrb(orbLocation, nextIndex);
                nextIndex++;
                //create orb for primary
                tillOrbSpawnTime = SPAWN_TIME;
            }
        }

    }

    //TODO
    private Vector3 calculateOrbLocation()
    {
        return new Vector3(0,0,nextIndex);
    }

    // todo fix and and adapt from ProcessRemoteSpell
    public void ProcessRemoteOrb(NetworkInMessage msg)
    {
        // userID not used for now
        long userID = msg.ReadInt64();
        Vector3 remoteOrbPosition = CustomMessages.Instance.ReadVector3(msg);
        int remoteIndex = msg.ReadInt32();

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        GenerateOrb(anchor.TransformPoint(remoteOrbPosition), remoteIndex);
    }

    public void RemoveRemoteOrb(NetworkInMessage msg)
    {
        // userID not used for now
        long userID = msg.ReadInt64();
        int remoteIndex = msg.ReadInt32();
        RemoveOrb(remoteIndex);
        
    }

    public void RemoveOrb(int index)
    {
        Destroy(Orbs[index]);
        Orbs[index] = null;
        Debug.Log("Pressed");
    }

    public void GenerateOrb(Vector3 loc, int index)
    {
        GameObject spawnedOrb = Instantiate(sampleOrb, loc, Quaternion.identity) as GameObject;
        spawnedOrb.transform.parent = this.transform;
        spawnedOrb.GetComponent<MeshRenderer>().enabled = true;
        spawnedOrb.GetComponent<Orb>().SetIndex(index);
        Orbs.Add(spawnedOrb);
    }
}