using NUnit.Framework;

public class EndSceneModelTests
{
    [Test]
    public void Constructor_SetsPlayerWon_True()
    {

        var model = new EndSceneModel(true);

        Assert.IsTrue(model.PlayerWon, "PlayerWon should be true");
    }

    [Test]
    public void Constructor_SetsPlayerWon_False()
    {

        var model = new EndSceneModel(false);

        Assert.IsFalse(model.PlayerWon, "PlayerWon should be false");
    }
}
