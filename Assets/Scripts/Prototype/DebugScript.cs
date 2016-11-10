using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VR.WSA;
public class DebugScript : MonoBehaviour {

    Text anchorPos;
    Text anchorState;
    Text Health;
    GameObject manager;

    void Start() {
        anchorPos = gameObject.transform.FindChild("AnchorPos").GetComponent<Text>();
        anchorState = gameObject.transform.FindChild("AnchorState").GetComponent<Text>();
        Health = gameObject.transform.FindChild("Health").GetComponent<Text>();
        manager = GameObject.Find("Anchor");
    }

	// Update is called once per frame
	void Update () {
        WorldAnchor anchor = manager.GetComponent<WorldAnchor>();
        ImportExportAnchorManager anchorMgr = manager.GetComponent<ImportExportAnchorManager>();
        if (anchor == null)
            anchorPos.text = "Anchor is null";
        else if (!anchor.isLocated)
            anchorPos.text = "Anchor is NOT located";
        else
            anchorPos.text = "AnchorPos : " + manager.transform.position;

        anchorState.text = "State : " + anchorMgr.StateName;

        Health.text = "Health: " + Player.Instance.getHealth();
    }
}
