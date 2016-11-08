using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour, Spell
{

    private string spellName = "incendio";
    private int damage = 10;
    private int mpCost = 1;

    public int getDamage()
    {
        return this.damage;
    }

    public int getMPCost()
    {
        return this.mpCost;
    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Entered here");
        if (other.collider.tag.Equals("MainCamera"))
        {
            // reduce player health
            Debug.Log("Hit the main camera");
            other.gameObject.GetComponent<Player>().modifyHealth(this.getDamage());
            Debug.Log("Current hp of player is: " + other.gameObject.GetComponent<Player>().getHealth());
        }
    }
    /*
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Entered trigger enter for fireball");
        if (collision.tag.Equals("MainCamera"))
        {
            
        }
    }*/
}