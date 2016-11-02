using NSubstitute;
using NUnit.Framework;

public class PlayerTest {

    [Test]
    public void HealthCanNotGoBelowZero() {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        Assert.AreEqual(100, health);
        health = player.modifyHealth(100);
        Assert.AreEqual(health, 0);
        health = player.modifyHealth(1);
        Assert.AreEqual(health, 0);
        Assert.AreEqual(player.getHealth(), 0);
    }

    [Test]
    public void HealthCanNotGoOverHundred()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        Assert.AreEqual(health, 100);
        health = player.modifyHealth(-1);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }

    [Test]
    public void TestNoDamage()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        Assert.AreEqual(health, 100);
        health = player.modifyHealth(0);
        Assert.AreEqual(health, 100);
        Assert.AreEqual(player.getHealth(), 100);
    }

    [Test]
    public void TestNormalDamage()
    {
        PlayerTestClass player = new PlayerTestClass();
        int health = player.getHealth();
        Assert.AreEqual(health, 100);
        health = player.modifyHealth(20);
        Assert.AreEqual(health, 80);
        Assert.AreEqual(player.getHealth(), 80);
    }

    [Test]
    public void TestMagicCanNotGoBelowZero()
    {
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        Assert.AreEqual(magic, 10);
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
        PlayerTestClass player = new PlayerTestClass();
        int magic = player.getMagic();
        Assert.AreEqual(magic, 10);
        magic = player.modifyMagic(-1);
        Assert.AreEqual(magic, 10);
        Assert.AreEqual(player.getMagic(), 10);
    }
}