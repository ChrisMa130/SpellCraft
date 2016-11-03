using NUnit.Framework;
using UnityEngine;

public class PlayerTest {

    [Test]
    public void TestPlayerCtor()
    {
        Player player = new Player();
        Assert.AreEqual(100, player.getHealth());
        Assert.AreEqual(10, player.getMagic());
        Assert.IsTrue(player.alive);
    }

    [Test]
    public void HealthCanNotGoBelowZero() {
        Player player = new Player();
        int health = player.getHealth();
        health = player.modifyHealth(100);
        Assert.AreEqual(health, 0);
        health = player.modifyHealth(1);
        Assert.AreEqual(health, 0);
        Assert.AreEqual(player.getHealth(), 0);
    }

    [Test]
    public void HealthCanNotGoOverHundred()
    {
        Player player = new Player();    
        int health = player.getHealth();
        health = player.modifyHealth(-1);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }

    [Test]
    public void TestNoDamage()
    {
        Player player = new Player();
        int health = player.getHealth();
        health = player.modifyHealth(0);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }

    [Test]
    public void TestNormalDamage()
    {
        Player player = new Player();
        int health = player.getHealth();
        health = player.modifyHealth(20);
        Assert.AreEqual(health, 80);
        Assert.AreEqual(player.getHealth(), 80);
    }

    [Test]
    public void TestDamageTwice()
    {
        Player player = new Player();
        int health = player.getHealth();
        player.modifyHealth(20);
        player.modifyHealth(30);
        health = player.getHealth();
        Assert.AreEqual(health, 50);
    }

    [Test]
    public void TestMagicCanNotGoBelowZero()
    {
        Player player = new Player();
        int magic = player.getMagic();
        magic = player.modifyMagic(10);
        Assert.AreEqual(magic, 0);
        Assert.AreEqual(player.getMagic(), 0);
        magic = player.modifyMagic(2);
        Assert.AreEqual(magic, -1);
        Assert.AreEqual(player.getMagic(), 0);
    }

    [Test]
    public void TestMagicCanNotGoOverTen()
    {
        Player player = new Player();
        int magic = player.getMagic();
        magic = player.modifyMagic(-1);
        Assert.AreEqual(magic, 10);
        Assert.AreEqual(player.getMagic(), 10);
    }

    [Test]
    public void TestMagicNormalCast()
    {
        Player player = new Player();
        int magic = player.getMagic();
        player.modifyMagic(1);
        magic = player.getMagic();
        Assert.AreEqual(magic, 9);
    }

    [Test]
    public void TestMagicCastTwo()
    {
        Player player = new Player();
        int magic = player.getMagic();
        player.modifyMagic(1);
        player.modifyMagic(1);
        magic = player.getMagic();
        Assert.AreEqual(magic, 8);
    }
}