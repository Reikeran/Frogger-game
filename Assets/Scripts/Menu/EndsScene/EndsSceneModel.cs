public class EndSceneModel
{
    public bool PlayerWon { get; private set; }

    public EndSceneModel(bool playerWon)
    {
        PlayerWon = playerWon;
    }
}
