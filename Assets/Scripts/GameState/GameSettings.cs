using UnityEngine;
using System.Collections;

/*
 * this class stores the 
 * 
 */

public class GameSettings : MonoBehaviour {

    public string IPAddress;

	void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
