using UnityEngine;
public class LightSpell : MonoBehaviour, Spell
{
    // stats of the light spell
    private string spellName = "reducto";
    private int damage = 5;
    private int mpCost = 1;

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