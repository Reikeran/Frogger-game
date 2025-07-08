using NUnit.Framework;
using UnityEngine;

public class ObstacleSpawnerModelTests
{
    private ObstacleSpawnerModel spawnerModel;
    private Vector3 minBounds = new Vector3(0, 0, 0);
    private Vector3 maxBounds = new Vector3(10, 10, 0);

    [SetUp]
    public void Setup()
    {
        spawnerModel = new ObstacleSpawnerModel(minBounds, maxBounds);
    }

    [Test]
    public void Constructor_SetsBoundsAndTimer()
    {
        Assert.AreEqual(minBounds, spawnerModel.MinBounds);
        Assert.AreEqual(maxBounds, spawnerModel.MaxBounds);
        Assert.AreEqual(0f, spawnerModel.SpawnTimer);
        Assert.AreEqual(0.3f, spawnerModel.SpawnInterval);
    }

    [Test]
    public void UpdateTimer_IncreasesSpawnTimer()
    {
        spawnerModel.UpdateTimer(0.1f);
        Assert.AreEqual(0.1f, spawnerModel.SpawnTimer);
        spawnerModel.UpdateTimer(0.2f);
        Assert.AreEqual(0.3f, spawnerModel.SpawnTimer);
    }

    [Test]
    public void CanSpawn_ReturnsTrueWhenTimerExceedsInterval()
    {
        spawnerModel.UpdateTimer(0.3f);
        Assert.IsTrue(spawnerModel.CanSpawn());

        spawnerModel = new ObstacleSpawnerModel(minBounds, maxBounds);
        spawnerModel.UpdateTimer(0.2f);
        Assert.IsFalse(spawnerModel.CanSpawn());
    }

    [Test]
    public void ResetTimer_DecreasesSpawnTimerByInterval()
    {
        spawnerModel.UpdateTimer(0.5f);
        spawnerModel.ResetTimer();
        Assert.AreEqual(0.0f, spawnerModel.SpawnTimer);
    }
}

