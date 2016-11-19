using NUnit.Framework;
using UnityEngine;

public class LightSpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestLightSpell() {
        LightSpellTestClass light = new LightSpellTestClass();
        Assert.IsNotNull(light);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        LightSpellTestClass light = new LightSpellTestClass();
        Assert.AreEqual(20, light.getDamage());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        LightSpellTestClass light = new LightSpellTestClass();
        Assert.AreEqual(2, light.getMPCost());
    }
}
