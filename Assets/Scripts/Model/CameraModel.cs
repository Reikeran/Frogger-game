using UnityEngine;

public class CameraModel
{
    public Vector3 Offset { get; private set; }

    public CameraModel(Vector3 offset)
    {
        Offset = offset;
    }

    public Vector3 GetCameraPosition(Vector3 playerPosition)
    {
        
        return new Vector3(Offset.x, playerPosition.y + Offset.y, Offset.z);
    }
}

