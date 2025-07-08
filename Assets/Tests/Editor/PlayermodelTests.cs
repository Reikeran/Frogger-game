using NUnit.Framework;
using UnityEngine;

public class PlayermodelTests
{
    private Playermodel player;
    private Vector3 startPos = new Vector3(0, 0, 0);
    private Vector3 minBounds = new Vector3(-5, -5, 0);
    private Vector3 maxBounds = new Vector3(5, 5, 0);

    [SetUp]
    public void SetUp()
    {
        player = new Playermodel(startPos, minBounds, maxBounds);
    }

    [Test]
    public void Constructor_SetsInitialPosition()
    {
        Assert.AreEqual(startPos, player.Position);
    }

    [Test]
    public void Move_WithinBounds_UpdatesPosition()
    {
        Vector3 direction = new Vector3(1, 1, 0);
        player.Move(direction);
        Assert.AreEqual(new Vector3(1, 1, 0), player.Position);
    }

    [Test]
    public void Move_ExceedsBounds_ClampsPosition()
    {
        Vector3 direction = new Vector3(10, 10, 0);
        player.Move(direction);
        Assert.AreEqual(new Vector3(5, 5, 0), player.Position);
    }

    [Test]
    public void UpdateDirection_SetsCorrectDirection()
    {
        player.UpdateDirection(Vector3.right);
        Assert.AreEqual(MoveDir.Right, player.LastDirection);

        player.UpdateDirection(Vector3.left);
        Assert.AreEqual(MoveDir.Left, player.LastDirection);

        player.UpdateDirection(Vector3.up);
        Assert.AreEqual(MoveDir.Up, player.LastDirection);

        player.UpdateDirection(Vector3.down);
        Assert.AreEqual(MoveDir.Down, player.LastDirection);
    }

    [Test]
    public void CheckWin_ReturnsTrue_WhenYEqualsMaxY()
    {
       
        player.Move(new Vector3(0, 5, 0));
        Assert.IsTrue(player.CheckWin());
    }

    [Test]
    public void CheckWin_ReturnsFalse_WhenYLessThanMaxY()
    {
        player.Move(Vector3.up);
        Assert.IsFalse(player.CheckWin());
    }
}
