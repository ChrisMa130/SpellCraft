using UnityEngine;
using System.Collections;

public interface Spell {

    // get damage of spell
    int getDamage();

    // get damage of spell
    int getMPCost();

    // reduce Player health on collision, and destroy spell instance
    void OnCollisionEnter(Collision collision);
}
