using UnityEngine;
using System.Collections;

/*
 * Model to represent the Orb during a game. Tracks an id to help keep track of it.
 * This id is called index and is simply the order in which it was created.
 */
public class Orb : MonoBehaviour {

    private const int MAGIC_STORED = 3;
    private int index;

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }



    // Instantiates an object of type Orb.
    // This is the data model that represents the current state of a player
    // within the game world. It tracks the index, which is the order it was created
    // Representation invariant: An index should be non-negative and unique
    public void Start () {
        Debug.Log("Starting orb debug");	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	// Modifies this Object to delete it. Also sends a message to the network for
    // others to delete it. Furthermore increments the colliding player's magic by
    // MAGIC_STORED
    // Parameters-Collider that ran into this Orb's Collider
	void OnTriggerEnter(Collider collision) {
        if (collision.tag.Equals("MainCamera"))
        {
            collision.gameObject.GetComponent<Player>().modifyMagic(MAGIC_STORED);

            CustomMessages.Instance.SendOrbPickedUpMessage(index);
            Destroy(gameObject);
            //broadcast a deystroy orb message
        }
        
	}
}
