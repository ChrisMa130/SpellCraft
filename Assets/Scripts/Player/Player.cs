using HoloToolkit.Unity;

public class Player : Singleton<Player> {

    private int MAX_HEALTH = 100;
    private int MAX_MAGIC = 10;

    // Looks like these are no longer needed. Review with team.
    //private bool gameStarted = false;  
    //private bool singlePlayer = false;

    private int health;
    private int magic;
	
    // Instantiates an object of type Player.
    // This is the data model that represents the current state of a player
    // within the game world. It tracks the health and magic points of the
    // player and reports those as requested to the appropriate game objects.
    // Representation invariant: A player must always have 0 < health <= 100
    //                           A player must always have 0 <= magic <= 10
	void Start ()
    {
        health = MAX_HEALTH;
        magic = MAX_MAGIC;
    }

    void Update()
    {
    }


	// Modify health by spell. If a spell is of the healing type, then the
    // argument passed to this will be a negative value, resulting in a
    // health increase.
    // Parameters- damage: the amount of damage done. Negative for healing
    // Returns- current health as an integer
	public int modifyHealth(int damage) {
        this.health -= damage;
        if (this.health < 0)
        {
            this.health = 0;
        } else if (this.health > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }
        return this.health;
	}

	// Modify mana by orbs or casting of spells. If the mana is being
    // increased, the argument is negative.
    // Parameters- magicPoints: the amount by which magic is changing. Negative
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
        magic -= magicPoints;
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

    // Returns the current magic points of the player.
    // Returns: magic points as an integer
    public int getMagic()
    {
        return this.magic;
    }
}