using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VR.WSA;
public class DebugScript : MonoBehaviour {

    Text anchorPos;
    Text anchorState;
    Text joinCount;
    Text Health;
    GameObject manager;

    void Start() {
        manager = GameObject.FindWithTag("Anchor");
        anchorPos = gameObject.transform.FindChild("AnchorPos").GetComponent<Text>();
        anchorState = gameObject.transform.FindChild("AnchorState").GetComponent<Text>();
        joinCount = gameObject.transform.FindChild("Join_Count").GetComponent<Text>();
        //Health = gameObject.transform.FindChild("Health").GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        WorldAnchor anchor = null;
        ImportExportAnchorManager anchorMgr = null;
        if (manager != null)
        {
            anchor = manager.GetComponent<WorldAnchor>();
            anchorMgr = manager.GetComponent<ImportExportAnchorManager>();
        }
        if (anchor == null)
            anchorPos.text = "Anchor is null";
        else if (!anchor.isLocated)
            anchorPos.text = "Anchor is NOT located";
        else
            anchorPos.text = "AnchorPos : " + manager.transform.position;

        anchorState.text = "State : " + anchorMgr.StateName;

        joinCount.text = "Joined Count : " + anchorMgr.join_count;

        //Health.text = "Health: " + Player.Instance.getHealth();
    }
}
