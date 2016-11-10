using HoloToolkit.Unity;
using UnityEngine;

public class Player : Singleton<Player>
{
    public int MAX_HEALTH = 100;
    public int MIN_HEALTH = 0;
    public int MAX_MAGIC = 10;

    // Looks like these are no longer needed. Review with team.
    //private bool gameStarted = false;  
    //private bool singlePlayer = false;

    private int health;
    private int magic;
    public bool alive;
    // Instantiates an object of type Player.
    // This is the data model that represents the current state of a player
    // within the game world. It tracks the health and magic points of the
    // player and reports those as requested to the appropriate game objects.
    // Representation invariant: A player must always have 0 < health <= 100
    //                           A player must always have 0 <= magic <= 10
    void Start()
    {
        health = MAX_HEALTH;
        magic = 10;
        alive = true;
    }

    public Player()
    {
        health = MAX_HEALTH;
        magic = MAX_MAGIC;
        alive = true;
    }

    void Update() { }

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

    // Returns the current magic points of the player.
    // Returns: magic points as an integer
    public int getMagic()
    {
        return this.magic;
    }

}