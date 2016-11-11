using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    public string IPAddress;

	void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
