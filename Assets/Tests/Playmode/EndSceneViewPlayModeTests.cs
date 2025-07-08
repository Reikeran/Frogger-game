using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class EndSceneViewPlayModeTests
{
    private GameObject go;
    private EndSceneView view;

    private class GameFlowManagerStub : MonoBehaviour
    {
        public static GameFlowManagerStub Instance { get; private set; }
        public bool PlayerWon;

        private void Awake()
        {
            Instance = this;
        }
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        var gmgo = new GameObject("GameFlowManager");
        var gmStub = gmgo.AddComponent<GameFlowManagerStub>();
        gmStub.PlayerWon = true;

        go = new GameObject("EndSceneView");
        view = go.AddComponent<EndSceneView>();

        var textGO = new GameObject("MessageText");
        textGO.transform.SetParent(go.transform);
        var tmpText = textGO.AddComponent<TextMeshProUGUI>();
        view.messageText = tmpText;

        var menuSceneNameField = typeof(EndSceneView).GetField("menuSceneName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        menuSceneNameField.SetValue(view, "MainMenu");

        view.Initialize();

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(go);
        var gm = GameObject.Find("GameFlowManager");
        if (gm != null) Object.Destroy(gm);
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnReturnToMenuButton_LoadsCorrectScene()
    {
        string sceneName = view.GetMenuSceneName();
        Assert.AreEqual("MainMenu", sceneName);

        view.OnReturnToMenuButton();

        yield return null;
    }

    [UnityTest]
    public IEnumerator OnQuitGameButton_CallsApplicationQuit()
    {
        view.OnQuitGameButton();

        yield return null;
    }
}
