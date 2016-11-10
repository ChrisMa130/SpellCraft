using UnityEngine;

public class HeavySpell : MonoBehaviour, Spell
{
    // stats of the heavy spell
    private string spellName = "confringo";
    private int damage = 50;
    private int mpCost = 10;

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
    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("MainCamera")) { }
        // reduce player health
        other.gameObject.GetComponent<Player>().modifyHealth(this.getDamage());
    }
}