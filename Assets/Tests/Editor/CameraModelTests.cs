using NUnit.Framework;
using UnityEngine;

public class CameraModelTests
{
    private CameraModel cameraModel;

    [SetUp]
    public void Setup()
    {
        Vector3 offset = new Vector3(5f, 10f, -15f);
        cameraModel = new CameraModel(offset);
    }

    [Test]
    public void Offset_IsSetCorrectly()
    {
        Assert.AreEqual(new Vector3(5f, 10f, -15f), cameraModel.Offset);
    }

    [Test]
    public void GetCameraPosition_ReturnsPositionWithCorrectOffset()
    {
        Vector3 playerPos = new Vector3(100f, 50f, 20f);
        Vector3 camPos = cameraModel.GetCameraPosition(playerPos);

        Assert.AreEqual(cameraModel.Offset.x, camPos.x);
        Assert.AreEqual(playerPos.y + cameraModel.Offset.y, camPos.y);
        Assert.AreEqual(cameraModel.Offset.z, camPos.z);
    }
}

