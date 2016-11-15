using UnityEngine;
using System.Collections;

public class MediumSpell : MonoBehaviour {

    // stats of the heavy spell
    //private string spellName = "confringo";
    private int damage = 34;
    private int mpCost = 5;

    void Awake()
    {
        GetComponent<EffectSettings>().CollisionEnter += Instance_OnCollisionEnter;
    }

    // get damage of spell
    public int getDamage()
    {
        return this.damage;
    }

    // get damage of spell
    public int getMPCost()
    {
        return this.mpCost;
    }

    // reduce Player health on collision, and destroy spell instance
    public void Instance_OnCollisionEnter(object sender, CollisionInfo e)
    {
        Debug.Log("OnCollisionEnter");
        if (e.Hit.collider.tag.Equals("MainCamera"))
        {
            Debug.Log("InsideFunction");
            // reduce player health
            e.Hit.collider.gameObject.GetComponent<Player>().modifyHealth(this.damage);
        }
    }
}
