﻿using UnityEngine;
using System.Collections;

public class LightSpell : MonoBehaviour {

    // stats of the heavy spell
    //private string spellName = "confringo";
    private int damage = 20;
    private int mpCost = 2;

    void Awake()
    {
        GetComponent<EffectSettings>().CollisionEnter += Instance_OnCollisionEnter;
    }

    // Get the damage of spell
    // Parameters- none
    // Returns- number of health points the spell removes from its target as an int
    public int getDamage()
    {
        return this.damage;
    }

    // Get the cost of spell
    // Parameters- none
    // Returns- number of magic points the spell removes from its caster as an int
    public int getMPCost()
    {
        return this.mpCost;
    }

    // reduce Player health on collision, and destroy spell instance
    public void Instance_OnCollisionEnter(object sender, CollisionInfo e)
    {
        Debug.Log("OnCollisionEnter");
        if (e.Hit.collider.tag.Equals("MainCamera"))
        {
            Debug.Log("InsideFunction");
            // reduce player health
            e.Hit.collider.gameObject.GetComponent<Player>().modifyHealth(this.damage);
        }
    }
}
