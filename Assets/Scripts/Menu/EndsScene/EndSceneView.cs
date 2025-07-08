using UnityEngine;
using TMPro;

public class EndSceneView : MonoBehaviour, IEndSceneView
{
    public TMP_Text messageText;
    [SerializeField] private string menuSceneName;

    private EndScenePresenter presenter;

    public void SetPresenter(EndScenePresenter presenter)
    {
        this.presenter = presenter;
    }

    public void SetMessage(string message)
    {
        if (messageText != null)
            messageText.text = message;
        else
            Debug.LogError("messageText no asignado en el inspector.");
    }

    public string GetMenuSceneName()
    {
        return menuSceneName;
    }

    public void Initialize()
    {
        if (GameFlowManager.Instance == null)
        {
            return;
        }

        bool playerWon = GameFlowManager.Instance.PlayerWon;
        EndSceneModel model = new EndSceneModel(playerWon);
        presenter = new EndScenePresenter(model, this);
    }

    public void OnReturnToMenuButton()
    {
        presenter?.OnReturnToMenu();
    }

    public void OnQuitGameButton()
    {
        presenter?.OnQuitGame();
    }

    private void Start()
    {
        Initialize();
    }
}
