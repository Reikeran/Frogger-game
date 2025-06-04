using UnityEngine;

public class CameraModel
{
    public Vector3 Offset { get; private set; }

    public CameraModel(Vector3 offset)
    {
        Offset = offset;
    }

    public Vector3 GetCameraPosition (Vector3 playerpos)
    {
        return new Vector3(0, playerpos.y, Offset.z);
    }
}
