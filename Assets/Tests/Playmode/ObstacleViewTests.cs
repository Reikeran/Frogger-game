using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ObstacleViewPlayModeTest
{
    [UnityTest]
    public IEnumerator ObstacleMovesRightWhenConfigured()
    {
       
        var go = new GameObject("Obstacle");
        var obstacle = go.AddComponent<ObstacleView>();

        Vector3 startPos = go.transform.position;
        Vector3 direction = Vector3.right;
        float speed = 5f;

        obstacle.SetMoveDirection(direction);
        obstacle.SetSpeed(speed);

        yield return null; 

    
        obstacle.Move();

 
        Vector3 newPos = go.transform.position;
        Assert.Greater(newPos.x, startPos.x, "Obstacle should have moved to the right.");
    }

    [UnityTest]
    public IEnumerator ObstacleDoesNotMoveWithZeroSpeed()
    {
        var go = new GameObject("Obstacle");
        var obstacle = go.AddComponent<ObstacleView>();

        Vector3 startPos = go.transform.position;
        obstacle.SetMoveDirection(Vector3.up);
        obstacle.SetSpeed(0f);

        yield return null;
        obstacle.Move();

        Vector3 newPos = go.transform.position;
        Assert.AreEqual(startPos, newPos, "Obstacle should not move with 0 speed.");
    }
}
