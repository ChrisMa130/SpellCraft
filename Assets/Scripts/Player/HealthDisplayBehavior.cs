using UnityEngine;
using System.Collections;

public class HealthDisplayBehavior : MonoBehaviour {

    public int health;
	
    // Use this for initialization
	void Start () {
        setHealth(100);
	}
	
    public void setHealth(int health)
    {
        this.health = health;
    }

	// Update is called once per frame
	void Update () {
        SpriteRenderer sprite = GetComponentInParent<SpriteRenderer>();

        // When health is at 1, should be R255 G0
        // When health is exactly at 50 should be R255 G255
        // When health is at 100, should be R0 G255
        Color color = new Color(0, 0, 0);
        if (health > 50)
        {
            float redModifier = (100f - health)/100f;
            redModifier = redModifier * 2;
            color = new Color(redModifier, 1f, 0);
        }
        else
        {
            float greenModifier = health / 50f;
            color = new Color(1f, greenModifier, 0);
        }
        sprite.color = color;
	}
}
