using NUnit.Framework;
using UnityEngine;

public class CameraPresenterTests
{
    private class MockCameraView : CameraView
    {
        private Vector3 position = Vector3.zero;

        public override Vector3 GetPosition() => position;
        public override void SetPosition(Vector3 pos) => position = pos;
    }

    private CameraPresenter presenter;
    private CameraModel cameraModel;
    private Playermodel playerModel;
    private MockCameraView mockView;

    [SetUp]
    public void SetUp()
    {
        var offset = new Vector3(0, 2, -10);
        cameraModel = new CameraModel(offset);

        Vector3 startPos = Vector3.zero;
        Vector3 minBounds = new Vector3(-10, -10, 0);
        Vector3 maxBounds = new Vector3(10, 10, 0);
        playerModel = new Playermodel(startPos, minBounds, maxBounds);

        mockView = new MockCameraView();

        presenter = new CameraPresenter(cameraModel, mockView, playerModel, smoothSpeed: 1f);
    }

    [Test]
    public void UpdateCamera_InterpolatesTowardsPlayerOffsetPosition()
    {
        playerModel.Move(Vector3.up * 3);
        float deltaTime = 1f;

        presenter.UpdateCamera(deltaTime);

        Vector3 expectedTarget = cameraModel.GetCameraPosition(playerModel.Position);
        Vector3 actual = mockView.GetPosition();

        Assert.AreNotEqual(Vector3.zero, actual);
        Assert.IsTrue(Vector3.Distance(actual, expectedTarget) < Vector3.Distance(Vector3.zero, expectedTarget));
    }
}

