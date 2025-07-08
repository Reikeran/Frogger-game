public class EndScenePresenter
{
    private EndSceneModel model;
    private IEndSceneView view;
    private ISceneService sceneService;

    public EndScenePresenter(EndSceneModel model, IEndSceneView view, ISceneService sceneService = null)
    {
        this.model = model;
        this.view = view;
        this.sceneService = sceneService ?? new UnitySceneService();

        view.SetPresenter(this);
        string message = model.PlayerWon ? "YOU WIN!" : "YOU LOSE";
        view.SetMessage(message);
    }

    public void OnReturnToMenu()
    {
        sceneService.LoadScene(view.GetMenuSceneName());
    }

    public void OnQuitGame()
    {
        sceneService.QuitApplication();
    }
}
