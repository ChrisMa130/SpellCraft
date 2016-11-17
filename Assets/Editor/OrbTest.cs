using NUnit.Framework;
using UnityEngine;

public class OrbTest
{

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void OrbPlayerCtor()
    {
        OrbTestClass orb = new OrbTestClass();
        Assert.AreEqual(0, orb.GetIndex());
    }

    /*
     * Ensure that get returns the most recent set for index
     */
    [Test]
    public void OrbRecentGetCheck()
    {
        OrbTestClass orb = new OrbTestClass();
        orb.SetIndex(5);
        Assert.AreEqual(5, orb.GetIndex());
        orb.SetIndex(4);
        Assert.AreEqual(4, orb.GetIndex());
    }
}