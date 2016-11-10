using UnityEngine;
using System.Collections;

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
	void Update () {
        updateColor();
        updateManaBalls();
    }

    public void setColor(Material mat, float r, float g, float b)
    {
        Color c = mat.color;
        c.r = r;
        c.g = g;
        c.b = b;
        mat.color = c;
    }

    public void set_hp(float p) {
        hpPercentage = p;
    }

    public void set_mp(int mp) {
        if (mp > nbBalls) { mp = nbBalls; }
        manaPoints = mp;
    }

    public void updateColor()
    {
        if (cursorMat != null)
        {
            setColor(cursorMat, 1 - hpPercentage, hpPercentage, hpPercentage / 2);
        }
    }

    public void updateManaBalls()
    {

        //if (mp > nbBalls) {mp = nbBalls;}

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
