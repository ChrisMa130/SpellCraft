using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SelectTest : MonoBehaviour {

	public void OnPlayPressed() {
		SceneManager.LoadScene("Prototype");
	}
}
