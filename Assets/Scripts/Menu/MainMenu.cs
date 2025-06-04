using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mainGame;

    public void StartGame()
    {
        SceneManager.LoadScene(mainGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
