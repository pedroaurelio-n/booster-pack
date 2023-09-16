using VContainer;

public class GameModel : IGameModel
{
    public ICardManagerModel CardManagerModel { get; private set; }

    public GameModel (ICardManagerModel cardManagerModel)
    {
        CardManagerModel = cardManagerModel;
    }

    public void Initialize ()
    {
    }
}