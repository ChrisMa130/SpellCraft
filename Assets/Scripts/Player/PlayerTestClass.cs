public class PlayerTestClass{
    // Class ctor used for testing purposes. Not called by the game itself.
    public int MAX_HEALTH = 100;
    public int MIN_HEALTH = 0;
    public int MAX_MAGIC = 10;

    private int health;
    private int magic;
    public bool alive;

    public PlayerTestClass() {
        health = MAX_HEALTH;
        magic = MAX_MAGIC;
        alive = true;
    }

    // Modify health by spell. If a spell is of the healing type, then the
    // argument passed to this will be a negative value, resulting in a
    // health increase.
    // Parameters- damage: the amount of damage done. Negative for healing
    // Returns- current health as an integer
    public int modifyHealth(int damage)
    {
        this.health -= damage;
        if (this.health < MIN_HEALTH)
        {
            this.health = MIN_HEALTH;
            this.alive = false;
            // Some kind of death happens now
            // Needs GameStateManager to display a 'defeated' scene to player
            // disconnect player there as well?
        }
        else if (this.health > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }

        return this.health;
    }

    // Modify mana by orbs or casting of spells. If the mana is being
    // increased, the argument is positive.
    // Parameters- magicPoints: the amount by which magic is changing. Positive
    //                          for orbs, indicating an increase
    // Returns- current magic as an integer. If for some reason this is called
    //          and the player does not have sufficient magic points to cast
    //          the requested spell, this will instead return -1.
    public int modifyMagic(int magicPoints)
    {

        if (magicPoints > magic)
        {
            return -1;
        }
        magic += magicPoints;
        if (magic > MAX_MAGIC)
        {
            magic = MAX_MAGIC;
        }
        return this.magic;
    }

    // Returns the current health of the player.
    // Returns: health as an integer
    public int getHealth()
    {
        return this.health;
    }

    // Returns current health of the player.
    // Returns: percentage of health as a float
    public float getHealthPercentage()
    {
        return (1.0f * health) / MAX_HEALTH;
    }

    // Returns the current magic points of the player.
    // Returns: magic points as an integer
    public int getMagic()
    {
        return this.magic;
    }
}
