using UnityEngine;

public class ObstacleSpawnerView : MonoBehaviour
{
    private ObstacleSpawnerPresenter presenter;
    private Collider2D playerCollider;
    private bool isActive = true;
    
    public void Init(ObstacleSpawnerPresenter presenter, Collider2D playerCollider)
    {
        this.presenter = presenter;
        this.playerCollider = playerCollider;
    }

    private void Update()
    {
        if (presenter != null && isActive)
        {
            presenter.OnUpdate(Time.deltaTime, false);
        }
    }
    public void SetActive(bool active)
    {
        isActive = active;
    }
    public virtual ObstacleView SpawnObstacle(Vector3 pos)
    {
        GameObject obsGO = Instantiate(obstaclePrefab, pos, Quaternion.identity);
        return obsGO.GetComponent<ObstacleView>();
    }

    [SerializeField] private GameObject obstaclePrefab;
}