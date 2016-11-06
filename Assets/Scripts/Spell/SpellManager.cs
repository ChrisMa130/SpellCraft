using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;

public class SpellManager : MonoBehaviour {

	public GameObject[] Spells;

	// Use this for initialization
	void Start () {
		CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.SpellMessage] = this.ProcessRemoteProjectile;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CastSpell(int index) {
        SpawnProjectile(0);
		//GameObject spell = Instantiate (Spells [index], Camera.main.transform.position + offset, Camera.main.transform.rotation) as GameObject;
		//spell.GetComponent<EffectSettings> ().Target = GameObject.Find ("BasicCursor");
        //Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
    }

    void SpawnProjectile(long UserId)
    {
		Vector3 offset = Camera.main.transform.forward * 0.5f;
		Vector3 pos = Camera.main.transform.position + offset;
        Vector3 curpos = GameObject.Find("BasicCursor").transform.position;
		Vector3 dir = curpos - pos;
		dir.Normalize ();
		dir = dir * 2f;
		ShootProjectile(pos, dir, UserId);

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        CustomMessages.Instance.SendSpell(anchor.InverseTransformPoint(pos), anchor.InverseTransformDirection(dir));
    }

    /// <summary>
    /// Adds a new projectile to the world.
    /// </summary>
    /// <param name="start">Position to shoot from</param>
    /// <param name="direction">Rotation to shoot toward</param>
    /// <param name="radius">Size of destruction when colliding.</param>
    void ShootProjectile(Vector3 start, Vector3 direction, long OwningUser)
    {
        // Just fireball. So spells[0]
		GameObject spawnedProjectile = (GameObject)Instantiate(Spells[0]) as GameObject;
        spawnedProjectile.transform.parent = this.transform;
        spawnedProjectile.transform.position = start;
        EffectSettings settings = spawnedProjectile.GetComponent<EffectSettings>();
        //  Vector3 curpos = GameObject.Find("BasicCursor").transform.position;
        //  Vector3 dir = curpos - start;
        if (OwningUser == 0)
        {
            settings.Target = GameObject.Find("BasicCursor");
        } else {
            settings.UseMoveVector = true;
            settings.MoveVector = direction;
        }

    
        /*
        ProjectileBehavior pc = spawnedProjectile.GetComponentInChildren<ProjectileBehavior>();
        pc.startDir = direction;
        pc.OwningUserId = OwningUser;*/
    }

    public void ProcessRemoteProjectile(NetworkInMessage msg)
    {
        long userID = msg.ReadInt64();
        Vector3 remoteProjectilePosition = CustomMessages.Instance.ReadVector3(msg);

        Vector3 remoteProjectileDirection = CustomMessages.Instance.ReadVector3(msg);

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        ShootProjectile(anchor.TransformPoint(remoteProjectilePosition), anchor.TransformDirection(remoteProjectileDirection), userID);
    }
}
