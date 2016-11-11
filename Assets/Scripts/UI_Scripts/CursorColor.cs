using UnityEngine;
using System.Collections;

/**
 * This class represents the color of the cursor
 * and the manaballs around the cursor
 * */
public class CursorColor : MonoBehaviour {

    private Material cursorMat;
    private int nbBalls;
    private Material[] manaBalls;
    private float hpPercentage;
    private int manaPoints;

	void Start () {
        cursorMat = gameObject.GetComponent<Renderer>().material;
        nbBalls = gameObject.transform.childCount;
        manaBalls = new Material[nbBalls];

        for (int i = 0; i < nbBalls; i++)
        {
            manaBalls[i] = gameObject.transform.GetChild(i).GetComponent<Renderer>().material;
        }
    }
	
	// Update is called once per frame
    /**
     * each frame, the color of the cursor has to be updated
     * and the number of lit mana balls should be reevaluated
     * */
	void Update () {
        updateColor();
        updateManaBalls();
    }

    /**
     * helper method that sets a meaterial's color's 
     * r,g,b to the ones given
     * */
    private void setColor(Material mat, float r, float g, float b)
    {
        Color c = mat.color;
        c.r = r;
        c.g = g;
        c.b = b;
        mat.color = c;
    }


    /**
     * setter which modifies the value of 
     * the percentage of health left
     * */
    public void set_hp(float p) {
        hpPercentage = p;
    }

    /**
     * setter which modifies the value of 
     * the mana points left
     * */
    public void set_mp(int mp) {
        if (mp > nbBalls) { mp = nbBalls; }
        manaPoints = mp;
    }

    /**
     * reset the color of the cursor depending on the hp
     * */
    private void updateColor()
    {
        if (cursorMat != null)
        {
            setColor(cursorMat, 1 - hpPercentage, hpPercentage, hpPercentage / 2);
        }
    }


    /**
     * recolors the mana balls depending on the mana points
     * */
    private void updateManaBalls()
    {


        for (int i = 0; i < manaPoints; i++)
        {
            setColor(manaBalls[i], 0f, .5f, 1f);
        }
        for (int i = manaPoints; i < nbBalls; i++)
        {
            setColor(manaBalls[i], 0f, 0f, 0f);
        }
    }

}
