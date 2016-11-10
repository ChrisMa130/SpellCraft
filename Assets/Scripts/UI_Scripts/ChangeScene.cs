using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void ChangeToScene(string sceneName)
    {
        int sceneCode = 0;

        switch (sceneName)
        {
            case "Prototype":
                {
                    sceneCode = 1; break;
                }
        }

        Application.LoadLevel(sceneCode);
    }
}
