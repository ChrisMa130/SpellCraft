using NUnit.Framework;
using UnityEngine;

public class MediumSpellTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestMediumSpell() {
        MediumSpellTestClass medium = new MediumSpellTestClass();
        Assert.IsNotNull(medium);
    }

    /*
     * Ensure the getDamage function returns the correct number.
     */
    [Test]
    public void getDamageReturnsCorrect() {
        MediumSpellTestClass medium = new MediumSpellTestClass();
        Assert.AreEqual(34, medium.getDamage());
    }

    /*
     * Ensure the getCost function returns the correct number.
     */
    [Test]
    public void getCostReturnsCorrect() {
        MediumSpellTestClass medium = new MediumSpellTestClass();
        Assert.AreEqual(5, medium.getMPCost());
    }
}
