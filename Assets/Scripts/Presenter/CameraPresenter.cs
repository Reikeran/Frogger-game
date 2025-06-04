using UnityEngine;

public class CameraPresenter
{
    private CameraModel model;
    private CameraView view;
    private Playermodel playerModel;
    private float smoothSpeed = 5f;

    public CameraPresenter(CameraModel model, CameraView view, Playermodel playerModel)
    {
        this.model = model;
        this.view = view;
        this.playerModel = playerModel;
    }

    public void UpdateCamera()
    {
        Vector3 targetPosition = model.GetCameraPosition(playerModel.Position);

        Vector3 currentCameraPosition = view.GetPosition();

        Vector3 smoothPosition = Vector3.Lerp(
            currentCameraPosition,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );
        smoothPosition.z = model.Offset.z;
        view.SetPosition(smoothPosition);
    }
}
