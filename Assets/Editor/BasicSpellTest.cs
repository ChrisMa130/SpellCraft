using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class BasicSpellTest {

	[Test]
	public void EditorTest()
	{
		//Arrange
		Spell testSpell = new Spell();
		//Assert
		//The object has a new name
		Assert.AreEqual(testSpell.getDamage(), 10);
	}
}
