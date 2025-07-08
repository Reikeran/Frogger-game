using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public string endgamescene;
    public bool PlayerWon { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EndGame(bool won)
    {
        PlayerWon = won;
        SoundManager.Instance.StopBGM();
        if (won)
        {
            SceneManager.LoadScene(endgamescene);
        }
        else
        {
            StartCoroutine(HandleLoseScreen());
        }
    }

    private System.Collections.IEnumerator HandleLoseScreen()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(endgamescene);
    }
}