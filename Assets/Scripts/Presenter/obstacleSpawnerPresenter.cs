using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerPresenter
{
    private ObstacleSpawnerModel model;
    private ObstacleSpawnerView view;
    private AudioClip crashClip;

    private List<ObstacleView> obstacles = new List<ObstacleView>();
    private Collider2D playerCollider;

    public event System.Action OnPlayerHit;

    public ObstacleSpawnerPresenter(ObstacleSpawnerModel model, ObstacleSpawnerView view, AudioClip crashClip, Collider2D playerCollider)
    {
        this.model = model;
        this.view = view;
        this.crashClip = crashClip;
        this.playerCollider = playerCollider;

        
    }

    public void OnUpdate(float deltaTime, bool isGameOver)
    {
        if (isGameOver) return;

        model.UpdateTimer(deltaTime);

        if (model.CanSpawn())
        {
            model.ResetTimer();
            TrySpawnObstacle();
        }

        MoveObstacles();
        CheckCollision();
    }

    private void TrySpawnObstacle()
    {
        bool spawnLeft = Random.value > 0.5f;
        float x = spawnLeft ? -10f : 10f;
        float y = Random.Range(model.MinBounds.y + 1f, model.MaxBounds.y - 1f);
        Vector3 spawnPos = new Vector3(x, y, 0);
        Vector3 moveDir = spawnLeft ? Vector3.right : Vector3.left;

        if (!IsValidSpawn(spawnPos) || IsBlockedByOpposite(spawnPos, moveDir))
        {
            model.SpawnTimer += model.SpawnInterval;
            return;
        }

        ObstacleView obstacle = view.SpawnObstacle(spawnPos);
        if (obstacle != null)
        {
            float speed = Random.Range(2.5f, 10.5f);
            obstacle.SetMoveDirection(moveDir);
            obstacle.SetSpeed(speed);
            obstacles.Add(obstacle);
        }
    }

    private void MoveObstacles()
    {
        obstacles.RemoveAll(o => o == null);
        foreach (var obstacle in obstacles)
        {
            obstacle.Move();
        }
    }

    private void CheckCollision()
    {
        if (playerCollider == null) return;

        foreach (var obstacle in obstacles)
        {
            if (obstacle == null) continue;

            var obstacleCollider = obstacle.GetComponent<Collider2D>();
            if (obstacleCollider != null && obstacleCollider.IsTouching(playerCollider))
            {
                SoundManager.Instance?.PlaySFX(crashClip);
                OnPlayerHit?.Invoke();
                break;
            }
        }
    }

    
    private bool IsValidSpawn(Vector3 pos, float minY = 3f)
    {
        foreach (var obstacle in obstacles)
        {
            if (obstacle == null) continue;
            float dy = Mathf.Abs(obstacle.transform.position.y - pos.y);
            float dx = Mathf.Abs(obstacle.transform.position.x - pos.x);
            if (dy < minY && dx < 2f)
                return false;
        }
        return true;
    }

    private bool IsBlockedByOpposite(Vector3 pos, Vector3 dir, float dist = 20f)
    {
        foreach (var obstacle in obstacles)
        {
            if (obstacle == null) continue;

            Vector3 otherDir = obstacle.GetMoveDirection();
            Vector3 otherPos = obstacle.transform.position;

            bool opposite = Vector3.Dot(dir, otherDir) < -0.9f;
            if (opposite && Mathf.Abs(otherPos.y - pos.y) < 1f)
            {
                float dx = pos.x - otherPos.x;
                if ((dir.x > 0 && dx < 0 || dir.x < 0 && dx > 0) && Mathf.Abs(dx) < dist)
                    return true;
            }
        }
        return false;
    }
}
