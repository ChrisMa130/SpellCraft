using NUnit.Framework;
using UnityEngine;

public class HeavySpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestHeavySpell() {
        HeavySpellTestClass heavy = new HeavySpellTestClass();
        Assert.IsNotNull(heavy);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        HeavySpellTestClass heavy = new HeavySpellTestClass();
        Assert.AreEqual(50, heavy.getDamage());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        HeavySpellTestClass heavy = new HeavySpellTestClass();
        Assert.AreEqual(10, heavy.getMPCost());
    }
}
