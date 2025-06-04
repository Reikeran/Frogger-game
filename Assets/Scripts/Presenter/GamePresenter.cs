using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GamePresenter: MonoBehaviour
{
    public PlayerView PlayerView;
    public GameObject ObstaclePrefab;
    public GameObject ExplosionPrefab;
    public GameObject minBoundspos;
    public GameObject maxBoundspos;

    [Header("AudioClip")]
    public AudioClip crashclip;

    private CameraPresenter cameraPresenter;
    private PlayerPresenter playerPresenter;
    private Playermodel playermodel;
    private GameModel gamemodel;
    private List<ObstacleView> ObstaclesInList;
    private float spawntimer;
    private const float SpawnCheckTime = 0.3f;


    public void Start()
    {
        Vector3 camoffset = new Vector3(0, 0, -10f);
        Vector3 startpos = new Vector3(0, minBoundspos.transform.position.y, 0);
        CameraModel camModel = new CameraModel(camoffset);
        CameraView camView = Camera.main.GetComponent<CameraView>();
        spawntimer = 0;
        playermodel = new Playermodel(startpos, minBoundspos.transform.position, maxBoundspos.transform.position);
        gamemodel = new GameModel();
        cameraPresenter = new CameraPresenter(camModel, camView, playermodel);
        playerPresenter = new PlayerPresenter(playermodel,PlayerView);
        ObstaclesInList = new List<ObstacleView>();
        gamemodel.OnPlayerHit += EndGame;
    }

    public void Update()
    {
        if (!gamemodel.IsGameOver ) 
        {
            playerPresenter.updatePresenter();
            checkWin();
            cameraPresenter.UpdateCamera();
            SpawnObstacles();
            CheckCollision();

        }
    }
    private void checkWin()
    {
        if (playermodel.CheckWin())
        {
            EndGame(true);
        }
    }
    bool IsSpawnPositionValid(Vector3 spawnPos, float minDistanceY = 3f)
    {
        foreach (var obs in ObstaclesInList)
        {
            if (obs == null) continue;

            float distanceY = Mathf.Abs(obs.transform.position.y - spawnPos.y);
            float distanceX = Mathf.Abs(obs.transform.position.x - spawnPos.x);

            if (distanceY < minDistanceY && distanceX < 2f)
            {
                return false;
            }
        }
        return true;
    }

    bool IsBlockedByOppositeObstacle(Vector3 spawnPos, Vector3 moveDir, float forwardCheckDistance = 20f)
    {
        foreach (var obs in ObstaclesInList)
        {
            if (obs == null) continue;

            Vector3 otherDir = obs.GetMoveDirection(); 
            Vector3 otherPos = obs.transform.position;

            bool isOpposite = Vector3.Dot(moveDir, otherDir) < -0.9f;

            if (isOpposite && Mathf.Abs(otherPos.y - spawnPos.y) < 1f)
            {
                float distX = spawnPos.x - otherPos.x;

                if (moveDir.x > 0 && distX < 0 && Mathf.Abs(distX) < forwardCheckDistance) return true;
                if (moveDir.x < 0 && distX > 0 && Mathf.Abs(distX) < forwardCheckDistance) return true;
            }
        }

        return false;
    }

    public void SpawnObstacles()
    {
        
        spawntimer += Time.deltaTime;
        if (spawntimer > SpawnCheckTime) 
        {
            spawntimer -= SpawnCheckTime;

            bool spawnOnLeft = Random.value > 0.5f;

            float xPos = spawnOnLeft ? -10f : 10f;
            int min = (int)(minBoundspos.transform.position.y + 1);
            int max = (int)(maxBoundspos.transform.position.y - 1);
            float yPos = Random.Range(min, max);
            Vector3 spawnPosition = new Vector2(xPos, yPos);
            Vector3 moveDirection = spawnOnLeft ? Vector3.right : Vector3.left;

            if (!IsSpawnPositionValid(spawnPosition) || IsBlockedByOppositeObstacle(spawnPosition, moveDirection))
            {
                spawntimer += SpawnCheckTime;
                return; 
            }
            GameObject obs = Instantiate(ObstaclePrefab, spawnPosition, Quaternion.identity);
            ObstacleView obstacleView = obs.GetComponent<ObstacleView>();
            GameObject.Destroy(obstacleView.gameObject, 7f);
            if (obstacleView != null)
            {
                obstacleView.SetMoveDirection(moveDirection);
                float velocity = Random.Range(2.5f, 10.5f);
                obstacleView.SetSpeed(velocity);
                ObstaclesInList.Add(obstacleView);
            }
            else
            {
                Debug.LogError("Obstacle prefab is missing ObstacleView component!");
            }
        }
        ObstaclesInList.RemoveAll(ob => ob == null);
        foreach (var obstacle in ObstaclesInList)
        {
            
            obstacle.Move();

        }
    }

    public void CheckCollision()
    {
        Collider2D playerCollider = PlayerView.GetComponent<Collider2D>();
        List<Collider2D> obstacleColliders = new List<Collider2D>();


       foreach (var obstacle in ObstaclesInList)
        {
            if (obstacle != null)
            {
                obstacleColliders.Add(obstacle.GetComponent<Collider2D>());
            }
        }
        gamemodel.CheckCollision(playerCollider, obstacleColliders, crashclip);
    }
    public void EndGame( bool won)
    {

        gamemodel.EndGame();
        if (!won)
        {
            Instantiate(ExplosionPrefab,playermodel.Position,Quaternion.identity);
        }
        GameFlowManager.Instance.EndGame(won);

    }
}
