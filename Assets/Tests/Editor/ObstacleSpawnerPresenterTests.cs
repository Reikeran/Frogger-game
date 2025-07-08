using NUnit.Framework;
using UnityEngine;

public class ObstacleSpawnerPresenterEditorTests
{
    private class DummyObstacleView : ObstacleView
    {
        public bool WasMoved { get; private set; } = false;

        public override void Move()
        {
            WasMoved = true;
        }
    }

    private class DummySpawnerView : ObstacleSpawnerView
    {
        public bool spawnCalled = false;
        public Vector3 lastPosition;

        public override ObstacleView SpawnObstacle(Vector3 position)
        {
            spawnCalled = true;
            lastPosition = position;

            GameObject go = new GameObject("DummyObstacle");
            var dummy = go.AddComponent<DummyObstacleView>();
            go.AddComponent<BoxCollider2D>();
            return dummy;
        }
    }

    [Test]
    public void ObstacleSpawner_SpawnsObstacle_WhenCanSpawn()
    {
        var minBounds = new Vector3(-5, -5, 0);
        var maxBounds = new Vector3(5, 5, 0);
        var model = new ObstacleSpawnerModel(minBounds, maxBounds);
        var dummyView = new GameObject("DummySpawnerView").AddComponent<DummySpawnerView>();
        var playerColliderGO = new GameObject("Player");
        playerColliderGO.AddComponent<BoxCollider2D>();

        var presenter = new ObstacleSpawnerPresenter(model, dummyView, null, playerColliderGO.GetComponent<Collider2D>());

        model.UpdateTimer(1.0f);
        presenter.OnUpdate(Time.deltaTime, isGameOver: false);
        Assert.IsTrue(dummyView.spawnCalled, "Expected SpawnObstacle.");
    }
}
