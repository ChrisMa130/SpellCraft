using UnityEngine;
public class MediumSpell : MonoBehaviour, Spell
{
    // stats of the medium spell
    //private string spellName = "incendio";
    private int damage = 25;
    private int mpCost = 5;

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
        if (other.collider.tag.Equals("MainCamera"))
        {
            // reduce player health
            other.gameObject.GetComponent<Player>().modifyHealth(this.getDamage());
        }
    }
}