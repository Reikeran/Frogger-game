using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public bool IsGameOver { get; private set; }

    public event System.Action<bool> OnGameEnd; // bool = ganó o perdió

    public void PlayerHit()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        OnGameEnd?.Invoke(false);
    }

    public void PlayerWin()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        OnGameEnd?.Invoke(true);
    }
}
