using NUnit.Framework;
using UnityEngine;

public class PlayerTest {

    /*
     * Ensure the constructor is working properly.
     */
    [Test]
    public void TestPlayerCtor()
    {
        PlayerTestClass player = new PlayerTestClass();
        Assert.AreEqual(100, player.getHealth());
        Assert.AreEqual(10, player.getMagic());
        Assert.IsTrue(player.alive);
    }

    /*
     * Ensure that the RI cannot be violated and health cannot be less
     * than zero.
     */
    [Test]
    public void HealthCanNotGoBelowZero() {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        health = player.modifyHealth(100);
        Assert.AreEqual(health, 0);
        health = player.modifyHealth(1);
        Assert.AreEqual(health, 0);
        Assert.AreEqual(player.getHealth(), 0);
    }

    /*
     * Ensure that the RI cannot be violated and health cannot be more
     * than 100.
     */
    [Test]
    public void HealthCanNotGoOverHundred()
    {
        PlayerTestClass player = new PlayerTestClass();    
        int health = player.getHealth();
        health = player.modifyHealth(-1);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }
    
    /*
     * Make sure zero damage works properly (none is dealt).
     */
    [Test]
    public void TestNoDamage()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        health = player.modifyHealth(0);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }

    /*
     * Make sure > 0 damage works properly.
     */
    [Test]
    public void TestNormalDamage()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        health = player.modifyHealth(20);
        Assert.AreEqual(health, 80);
        Assert.AreEqual(player.getHealth(), 80);
    }

    /*
     * Test two hits in a row.
     */
    [Test]
    public void TestDamageTwice()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        player.modifyHealth(20);
        player.modifyHealth(30);
        health = player.getHealth();
        Assert.AreEqual(health, 50);
    }
    /*
     * Test health percentage
     */
     [Test]
     public void TestHealthPercentage()
    {
        PlayerTestClass player = new PlayerTestClass();
        player.modifyHealth(50);
        Assert.AreEqual(.5f, player.getHealthPercentage());
    }

    /*
     * Ensure that RI can not be violated and magic can not go below
     * zero.
     */
    [Test]
    public void TestMagicCanNotGoBelowZero()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        magic = player.modifyMagic(-10);
        Assert.AreEqual(magic, 0);
        Assert.AreEqual(player.getMagic(), 0);
        magic = player.modifyMagic(-2);
        Assert.AreEqual(magic, -1);
        Assert.AreEqual(player.getMagic(), 0);
    }

    /*
     * Regression test. Make sure that at zero magic, the player
     * can still get orbs/regenerate spell points.
     */
    [Test]
    public void TestOrbPickupAtZeroMagic()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        magic = player.modifyMagic(-10);
        Assert.AreEqual(player.getMagic(), 0);
        player.modifyMagic(1);
        Assert.AreEqual(player.getMagic(), 1);
    }

    /*
     * Ensure that RI cannot be violated and magic can not go above
     * 100.
     */
    [Test]
    public void TestMagicCanNotGoOverTen()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        magic = player.modifyMagic(1);
        Assert.AreEqual(magic, 10);
        Assert.AreEqual(player.getMagic(), 10);
    }

    /*
     * Make sure magic decrements properly when a spell would be cast.
     */
    [Test]
    public void TestMagicNormalCast()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        player.modifyMagic(-1);
        magic = player.getMagic();
        Assert.AreEqual(magic, 9);
    }

    /*
     * Cast two spells.
     */
    [Test]
    public void TestMagicCastTwo()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        player.modifyMagic(-1);
        player.modifyMagic(-1);
        magic = player.getMagic();
        Assert.AreEqual(magic, 8);
    }
}