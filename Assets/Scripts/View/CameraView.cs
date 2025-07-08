using UnityEngine;

public class CameraView : MonoBehaviour
{
    private CameraPresenter presenter;

    public void Init(CameraPresenter presenter)
    {
        this.presenter = presenter;
    }

    private void Update()
    {
        presenter?.UpdateCamera(Time.deltaTime);
    }

    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }
}
