using UnityEngine;

public class MainMenuPresenter
{
    private MainMenuModel model;
    private IMainMenuView view;

    public MainMenuPresenter(MainMenuModel model, IMainMenuView view)
    {
        this.model = model;
        this.view = view;
        view.SetPresenter(this);
    }

    public void OnStartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(model.GameSceneName);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
