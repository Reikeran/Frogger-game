using UnityEngine;

public class CameraPresenter
{
    private CameraModel model;
    private CameraView view;
    private Playermodel playerModel;
    private float smoothSpeed;

    public CameraPresenter(CameraModel model, CameraView view, Playermodel playerModel, float smoothSpeed = 5f)
    {
        this.model = model;
        this.view = view;
        this.playerModel = playerModel;
        this.smoothSpeed = smoothSpeed;
    }

    public void UpdateCamera(float deltaTime)
    {
        Vector3 targetPos = model.GetCameraPosition(playerModel.Position);
        Vector3 currentPos = view.GetPosition();
        Vector3 smoothPos = Vector3.Lerp(currentPos, targetPos, deltaTime * smoothSpeed);
        view.SetPosition(smoothPos);
    }
}

