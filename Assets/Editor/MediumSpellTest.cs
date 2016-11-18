using NUnit.Framework;
using UnityEngine;

public class MediumSpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestMediumSpell() {
        MediumSpellTestClass medium = new MediumTestClass();
        Assert.isInstanceOf(medium, MediumTestClass);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        MediumSpellTestClass medium = new MediumTestClass();
        Assert.AreEqual(34, player.getHealth());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        MediumSpellTestClass medium = new MediumTestClass();
        Assert.AreEqual(5, player.getHealth());
    }
}
