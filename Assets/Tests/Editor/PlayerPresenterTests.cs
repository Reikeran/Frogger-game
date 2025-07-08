using NUnit.Framework;
using UnityEngine;

public class PlayerPresenterTests
{
    private PlayerPresenter presenter;
    private Playermodel model;
    private FakePlayerView view;
    private GameObject go;

    private class DummySoundManager : SoundManager
    {
        public override void PlaySFX(AudioClip clip) {  }
    }

    private class FakePlayerView : PlayerView
    {
        public Vector3 LastPosition { get; private set; }
        public MoveDir LastDirection { get; private set; }

        public override void SetPosition(Vector3 position)
        {
            LastPosition = position;
        }

        public override void UpdateSprite(MoveDir dir)
        {
            LastDirection = dir;
        }
    }

    [SetUp]
    public void SetUp()
    {
        
        go = new GameObject("FakePlayerView");
        view = go.AddComponent<FakePlayerView>();

        
        go.AddComponent<SpriteRenderer>();
        view.stepclip = AudioClip.Create("step", 44100, 1, 44100, false);

        
        var dummySMGO = new GameObject("DummySoundManager");
        dummySMGO.AddComponent<DummySoundManager>();
        var field = typeof(SoundManager).GetProperty("Instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        var backingField = typeof(SoundManager).GetField("<Instance>k__BackingField", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        backingField.SetValue(null, dummySMGO.GetComponent<SoundManager>());

        
        model = new Playermodel(Vector3.zero, new Vector3(-10, -10, 0), new Vector3(10, 10, 0));
        presenter = new PlayerPresenter(model, view);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
        var smGO = GameObject.Find("DummySoundManager");
        if (smGO) Object.DestroyImmediate(smGO);
    }

    [Test]
    public void OnInput_MoveUp_UpdatesModelAndView()
    {
        presenter.OnInput(Vector3.up);

        Assert.AreEqual(new Vector3(0, 1, 0), model.Position);
        Assert.AreEqual(new Vector3(0, 1, 0), view.LastPosition);
        Assert.AreEqual(MoveDir.Up, view.LastDirection);
    }

    [Test]
    public void OnInput_DisabledInput_DoesNothing()
    {
        presenter.SetInputEnabled(false);
        presenter.OnInput(Vector3.right);

        Assert.AreEqual(Vector3.zero, model.Position);
        Assert.AreEqual(default(Vector3), view.LastPosition);
    }
}
