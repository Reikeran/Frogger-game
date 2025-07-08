using NUnit.Framework;
using UnityEngine;

public class CameraViewTests
{
    private GameObject cameraGO;
    private CameraView cameraView;

    [SetUp]
    public void Setup()
    {
        cameraGO = new GameObject("CameraView");
        cameraView = cameraGO.AddComponent<CameraView>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cameraGO);
    }

    [Test]
    public void SetPosition_UpdatesTransformPosition()
    {
        Vector3 testPosition = new Vector3(1, 2, -10);
        cameraView.SetPosition(testPosition);

        Assert.AreEqual(testPosition, cameraGO.transform.position);
    }

    [Test]
    public void GetPosition_ReturnsCurrentTransformPosition()
    {
        Vector3 expected = new Vector3(5, 3, -8);
        cameraGO.transform.position = expected;

        Assert.AreEqual(expected, cameraView.GetPosition());
    }
}
