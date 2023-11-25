public class GameModel : IGameModel
{
    public ICardManagerModel CardManagerModel { get; private set; }
    public IBoosterPackModel BoosterPackModel { get; private set; }

    public GameModel (
        ICardManagerModel cardManagerModel, 
        IBoosterPackModel boosterPackModel
    )
    {
        CardManagerModel = cardManagerModel;
        BoosterPackModel = boosterPackModel;
    }

    public void Initialize ()
    {
    }
}