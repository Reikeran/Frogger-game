public class MainMenuModel
{
    public string GameSceneName { get; private set; }

    public MainMenuModel(string gameSceneName)
    {
        GameSceneName = gameSceneName;
    }
}