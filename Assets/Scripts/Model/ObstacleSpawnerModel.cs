using UnityEngine;

public class ObstacleSpawnerModel
{
    public float SpawnTimer { get; set; }
    public float SpawnInterval { get; private set; } = 0.3f;
    public Vector3 MinBounds { get; private set; }
    public Vector3 MaxBounds { get; private set; }

    public ObstacleSpawnerModel(Vector3 minBounds, Vector3 maxBounds)
    {
        MinBounds = minBounds;
        MaxBounds = maxBounds;
        SpawnTimer = 0f;
    }

    public void UpdateTimer(float deltaTime)
    {
        SpawnTimer += deltaTime;
    }

    public bool CanSpawn()
    {
        return SpawnTimer >= SpawnInterval;
    }

    public void ResetTimer()
    {
        SpawnTimer = 0;
    }
}
