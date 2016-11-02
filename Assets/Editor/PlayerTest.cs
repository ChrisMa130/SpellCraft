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
}