using NUnit.Framework;
using System;

public class GameModelTests
{
    private GameModel gameModel;
    private bool gameEnded;
    private bool? result;

    [SetUp]
    public void Setup()
    {
        gameModel = new GameModel();
        gameEnded = false;
        result = null;

        gameModel.OnGameEnd += (won) => {
            gameEnded = true;
            result = won;
        };
    }

    [Test]
    public void InitialState_IsGameOver_IsFalse()
    {
        Assert.IsFalse(gameModel.IsGameOver);
    }

    [Test]
    public void PlayerHit_SetsIsGameOver_AndInvokesEventWithFalse()
    {
        gameModel.PlayerHit();

        Assert.IsTrue(gameModel.IsGameOver);
        Assert.IsTrue(gameEnded);
        Assert.IsFalse(result.Value);
    }

    [Test]
    public void PlayerWin_SetsIsGameOver_AndInvokesEventWithTrue()
    {
        gameModel.PlayerWin();

        Assert.IsTrue(gameModel.IsGameOver);
        Assert.IsTrue(gameEnded);
        Assert.IsTrue(result.Value);
    }

    [Test]
    public void PlayerHit_AfterGameOver_DoesNotInvokeEventAgain()
    {
        gameModel.PlayerHit();
        gameEnded = false;

        gameModel.PlayerHit();

        Assert.IsFalse(gameEnded);
    }

    [Test]
    public void PlayerWin_AfterGameOver_DoesNotInvokeEventAgain()
    {
        gameModel.PlayerWin();
        gameEnded = false;

        gameModel.PlayerWin();

        Assert.IsFalse(gameEnded);
    }
}
