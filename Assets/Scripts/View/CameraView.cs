using UnityEngine;

public class CameraView : MonoBehaviour
{
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
