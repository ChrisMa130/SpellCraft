// Class used for testing purposes. Not called by the game itself.
public class LightSpellTestClass {

    // stats of the heavy spell
    //private string spellName = "confringo";
    private int damage;
    private int mpCost;

    public LightSpellTestClass() {
        damage = 20;
        mpCost = 2;
    }

    // Get the damage of spell
    // Parameters- none
    // Returns- number of health points the spell removes from its target as an int
    public int getDamage() {
        return this.damage;
    }

    // Get the cost of spell
    // Parameters- none
    // Returns- number of magic points the spell removes from its caster as an int
    public int getMPCost() {
        return this.mpCost;
    }
}
