public interface IEndSceneView
{
    void SetPresenter(EndScenePresenter presenter);
    void SetMessage(string message);
    string GetMenuSceneName();
}
