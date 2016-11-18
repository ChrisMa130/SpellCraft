using NUnit.Framework;
using UnityEngine;

public class LightSpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestLightSpell() {
        LightSpellTestClass light = new LightTestClass();
        Assert.isInstanceOf(light, LightTestClass);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        LightSpellTestClass light = new LightTestClass();
        Assert.AreEqual(20, player.getHealth());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        LightSpellTestClass light = new LightTestClass();
        Assert.AreEqual(2, player.getHealth());
    }
}
