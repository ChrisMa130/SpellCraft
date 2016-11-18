using NUnit.Framework;
using UnityEngine;

public class HeavySpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestHeavySpell() {
        HeavySpellTestClass heavy = new HeavyTestClass();
        Assert.IsInstanceOf(heavy, HeavyTestClass);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        HeavySpellTestClass heavy = new HeavyTestClass();
        Assert.AreEqual(50, player.getHealth());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        HeavySpellTestClass heavy = new HeavyTestClass();
        Assert.AreEqual(10, player.getHealth());
    }
}
