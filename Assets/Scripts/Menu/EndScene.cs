using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    public TMP_Text messageText;      // Assign in inspector
    public string sceneToChange;

    void Start()
    {
        if (GameFlowManager.Instance == null) return;

        bool won = GameFlowManager.Instance.PlayerWon;
        messageText.text = won ? "YOU WIN!" : "YOU LOSE";

        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(sceneToChange);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
