using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;

/**
 * This class is used to manage the casting of the game's spells.
 **/
public class SpellManager : MonoBehaviour {

    // 0 = fire bolt, 1 = electric bolt, 2 = fire phoenix
	public GameObject[] Spells;

	// Use this for initialization
	void Start () {
		CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.SpellMessage] = this.ProcessRemoteProjectile;
    }
	
	// Update is called once per frame. Currently does nothing.
	void Update () {
	
	}

    /*
     * Specifies which spell is being cast. Also performs a check against the player model to ensure that
     * the casting player has enough magic points to be able to cast the spell. If the player has insufficient
     * magic points, this does nothing.
     * 
     * @param spellIndex the index of the spell being cast
     */
	public void CastSpell(int spellIndex) {
        Player player = Player.Instance;
        Projectile newSpell = Spells[spellIndex].GetComponent<Projectile>();
        if (player.getMagic() >= newSpell.mpCost)
        {
            player.modifyMagic(newSpell.mpCost * -1);
            SpawnProjectile(0, spellIndex);
        }
		//GameObject spell = Instantiate (Spells [spellIndex], Camera.main.transform.position + offset, Camera.main.transform.rotation) as GameObject;
		//spell.GetComponent<EffectSettings> ().Target = GameObject.Find ("BasicCursor");
        //Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
    }

    /*
     * Spawns the projectile of the spell which has been cast. This is used both locally (for the player's own
     * spells).
     * 
     * @param UserID - the id of the casting user (0 if local)
     * @param spellIndex - the index of the spell being cast
     */

    void SpawnProjectile(long UserId, int spellIndex)
    {
		Vector3 offset = Camera.main.transform.forward * 0.5f;
		Vector3 pos = Camera.main.transform.position + offset;
        Vector3 curpos = GameObject.Find("BasicCursor").transform.position;
		Vector3 dir = curpos - pos;
		dir.Normalize ();
		dir = dir * 2f;
		ShootProjectile(pos, dir, UserId, spellIndex);

        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        CustomMessages.Instance.SendSpell(anchor.InverseTransformPoint(pos), anchor.InverseTransformDirection(dir), spellIndex);
    }

    /// <summary>
    /// Adds a new projectile to the world.
    /// </summary>
    /// <param name="start">Position to shoot from</param>
    /// <param name="direction">Rotation to shoot toward</param>
    /// <param name="radius">Size of destruction when colliding.</param>
    void ShootProjectile(Vector3 start, Vector3 direction, long OwningUser, int spellIndex)
    {
        // create a GameObject with the appropriate graphics for the spell being cast
		GameObject spawnedProjectile = (GameObject)Instantiate(Spells[spellIndex]) as GameObject;
        spawnedProjectile.transform.parent = this.transform;
        spawnedProjectile.transform.position = start;
        EffectSettings settings = spawnedProjectile.GetComponent<EffectSettings>();
        //  Vector3 curpos = GameObject.Find("BasicCursor").transform.position;
        //  Vector3 dir = curpos - start;
        if (OwningUser == 0)
        {
            settings.Target = GameObject.Find("BasicCursor");
        } else {
            spawnedProjectile.layer = 0;
            settings.UseMoveVector = true;
            settings.MoveVector = direction;
        }

    
        /*
        ProjectileBehavior pc = spawnedProjectile.GetComponentInChildren<ProjectileBehavior>();
        pc.startDir = direction;
        pc.OwningUserId = OwningUser;*/
    }

    /*
     * Used to process a spell cast from another player
     */
    public void ProcessRemoteProjectile(NetworkInMessage msg)
    {
        long userID = msg.ReadInt64();
        Vector3 remoteProjectilePosition = CustomMessages.Instance.ReadVector3(msg);

        Vector3 remoteProjectileDirection = CustomMessages.Instance.ReadVector3(msg);

        int spellIndex = msg.ReadInt32();
        Transform anchor = ImportExportAnchorManager.Instance.gameObject.transform;
        ShootProjectile(anchor.TransformPoint(remoteProjectilePosition), anchor.TransformDirection(remoteProjectileDirection), userID, spellIndex);
    }
}
