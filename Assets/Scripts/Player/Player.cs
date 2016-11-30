using HoloToolkit.Unity;
using UnityEngine;

/*
 * Model to represent the Player during a game. Tracks health and magic points
 * of the player. Health is an integer from 0 to 100, with game over occuring
 * for this player at 0. Magic points is an integer from 0 to 10.
 */
public class Player : Singleton<Player>
{
    public int MAX_HEALTH = 100;
    public int MIN_HEALTH = 0;
    public int MAX_MAGIC = 10;

    private int health;
    private int magic;
    public bool alive;
    private Game_UIManager uiMgr;
    // Instantiates an object of type Player.
    // This is the data model that represents the current state of a player
    // within the game world. It tracks the health and magic points of the
    // player and reports those as requested to the appropriate game objects.
    // Representation invariant: A player must always have 0 < health <= 100
    //                           A player must always have 0 <= magic <= 10
    void Start()
    {
        health = MAX_HEALTH;
        magic = MAX_MAGIC;
        alive = true;
        uiMgr = GameObject.FindWithTag("GameUI").GetComponent<Game_UIManager>();
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
    // increased, the argument is positive.
    // Parameters- magicPoints: the amount by which magic is changing. Positive
    //                          for orbs, indicating an increase
    // Returns- current magic as an integer. If for some reason this is called
    //          and the player does not have sufficient magic points to cast
    //          the requested spell, this will instead return -1.
    public int modifyMagic(int magicPoints)
    {
        if (magicPoints < 0)
        {
            if (Mathf.Abs(magicPoints) > magic)
            {
                return -1;
            }
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

	public float getHealthPercentage(){
		return (1.0f * health) / MAX_HEALTH;
	}

    // Returns the current magic points of the player.
    // Returns: magic points as an integer
    public int getMagic()
    {
        return this.magic;
    }

    //These are used for UI testing purposes ONLY
    public void hitMe()
    {
        modifyHealth(5);
        if (!alive) {
            uiMgr.GameEnded(false);
        }
    }
    public void healMe()
    {
        modifyHealth(-5);
    }

    public void useMana()
    {
        if (magic > 0) { magic--; }
        else { uiMgr.GameEnded(true); }
    }

    public void rechargeMana()
    {
        if (magic < MAX_MAGIC) { magic++; }
    }
    public void destroyCanvas() {
        uiMgr.DestroyCanvas();
    }



}