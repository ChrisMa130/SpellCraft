using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VR.WSA;
public class DebugScript : MonoBehaviour {

    Text anchorPos;
    Text anchorState;
    Text Join_Count;
    GameObject manager;

    void Start() {
        anchorPos = gameObject.transform.FindChild("AnchorPos").GetComponent<Text>();
        anchorState = gameObject.transform.FindChild("AnchorState").GetComponent<Text>();
        Join_Count = gameObject.transform.FindChild("Join_Count").GetComponent<Text>();
        manager = GameObject.Find("SpellManager");
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

        Join_Count.text = "Join_Count : " + anchorMgr.join_count;
    }
}
