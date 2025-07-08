using UnityEngine;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private string gameSceneName;

    private MainMenuPresenter presenter;

    public void SetPresenter(MainMenuPresenter presenter)
    {
        this.presenter = presenter;
    }

    // Métodos que se asignan a los botones UI

    public void OnStartGameButton()
    {
        presenter?.OnStartGame();
    }

    public void OnQuitGameButton()
    {
        presenter?.OnQuitGame();
    }

    void Start()
    {
        var model = new MainMenuModel(gameSceneName);
        presenter = new MainMenuPresenter(model, this);
    }
}
