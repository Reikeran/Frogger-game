using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;

public class GameModel
{
    public bool IsGameOver {  get; private set; }
    public Action<bool> OnPlayerHit;
    public void CheckCollision(Collider2D playerCollider, List<Collider2D> obstacleColliders, AudioClip collisionSound)
    {
        foreach (var obstacle in obstacleColliders)
        {
            if (playerCollider.bounds.Intersects(obstacle.bounds))
            {
                SoundManager.Instance.PlaySFX(collisionSound);
                OnPlayerHit?.Invoke(false);
                EndGame();
                return;
            }
        }
    }
    public void EndGame()
    {
        IsGameOver = true;
    }
    public void ResetGame()
    {
        IsGameOver = false;
    }
}
