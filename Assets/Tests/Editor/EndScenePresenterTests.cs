using NUnit.Framework;

public class EndScenePresenterTests
{
    private class DummyView : IEndSceneView
    {
        public string MessageSet;
        public string MenuSceneName = "MainMenu";
        public EndScenePresenter PresenterSet;

        public void SetPresenter(EndScenePresenter presenter)
        {
            PresenterSet = presenter;
        }

        public void SetMessage(string message)
        {
            MessageSet = message;
        }

        public string GetMenuSceneName()
        {
            return MenuSceneName;
        }
    }

    private class MockSceneService : ISceneService
    {
        public string LoadedScene;
        public bool QuitCalled;

        public void LoadScene(string sceneName)
        {
            LoadedScene = sceneName;
        }

        public void QuitApplication()
        {
            QuitCalled = true;
        }
    }

    [Test]
    public void Constructor_SetsWinMessage_WhenPlayerWon()
    {
        var model = new EndSceneModel(true);
        var view = new DummyView();
        var service = new MockSceneService();

        new EndScenePresenter(model, view, service);

        Assert.AreEqual("YOU WIN!", view.MessageSet);
    }

    [Test]
    public void Constructor_SetsLoseMessage_WhenPlayerLost()
    {
        var model = new EndSceneModel(false);
        var view = new DummyView();
        var service = new MockSceneService();

        new EndScenePresenter(model, view, service);

        Assert.AreEqual("YOU LOSE", view.MessageSet);
    }

    [Test]
    public void OnReturnToMenu_CallsLoadScene()
    {
        var model = new EndSceneModel(true);
        var view = new DummyView { MenuSceneName = "MainMenu" };
        var service = new MockSceneService();

        var presenter = new EndScenePresenter(model, view, service);
        presenter.OnReturnToMenu();

        Assert.AreEqual("MainMenu", service.LoadedScene);
    }

    [Test]
    public void OnQuitGame_CallsQuitApplication()
    {
        var model = new EndSceneModel(false);
        var view = new DummyView();
        var service = new MockSceneService();

        var presenter = new EndScenePresenter(model, view, service);
        presenter.OnQuitGame();

        Assert.IsTrue(service.QuitCalled);
    }
}
