using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

    private const int MAGIC_STORED = 1;
    private int index;

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    

	// Use this for initialization
	public void Start () {
        Debug.Log("Starting orb debug");	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	// increase mp for player with collide
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
