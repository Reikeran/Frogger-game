using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerView PlayerView;
    public ObstacleSpawnerView SpawnerView;
    public CameraView CameraView;

    public GameObject minBoundspos;
    public GameObject maxBoundspos;

    public GameObject ExplosionPrefab;
    public AudioClip crashclip;

    private Playermodel playermodel;
    private GameModel gamemodel;
    private PlayerPresenter playerPresenter;
    private CameraPresenter cameraPresenter;
    private ObstacleSpawnerPresenter spawnerPresenter;

    private bool hasEnded = false;

    void Start()
    {
        Vector3 minBounds = minBoundspos.transform.position;
        Vector3 maxBounds = maxBoundspos.transform.position;
        Vector3 startPos = new Vector3(0, minBounds.y, 0);
        Vector3 cameraOffset = new Vector3(0, 0, -10f);

        
        playermodel = new Playermodel(startPos, minBounds, maxBounds);
        gamemodel = new GameModel();

        
        playerPresenter = new PlayerPresenter(playermodel, PlayerView);
        playerPresenter.OnPlayerWin += () => gamemodel.PlayerWin();
        PlayerView.Init(playerPresenter);

        
        CameraModel camModel = new CameraModel(cameraOffset);
        cameraPresenter = new CameraPresenter(camModel, CameraView, playermodel);
        CameraView.Init(cameraPresenter);

       
        ObstacleSpawnerModel spawnerModel = new ObstacleSpawnerModel(minBounds, maxBounds);
        Collider2D playerCollider = PlayerView.GetComponent<Collider2D>();
        spawnerPresenter = new ObstacleSpawnerPresenter(spawnerModel, SpawnerView, crashclip, playerCollider);
        SpawnerView.Init(spawnerPresenter, playerCollider);

       
        spawnerPresenter.OnPlayerHit += () => gamemodel.PlayerHit();
        gamemodel.OnGameEnd += EndGame;
    }

    private void EndGame(bool won)
    {
        if (hasEnded) return;
        hasEnded = true;

        playerPresenter.SetInputEnabled(false);
        SpawnerView.SetActive(false);

        if (!won && ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, playermodel.Position, Quaternion.identity);
        }

        GameFlowManager.Instance?.EndGame(won);
    }
}
