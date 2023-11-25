public interface IGameModel
{
    ICardManagerModel CardManagerModel { get; }
    IBoosterPackModel BoosterPackModel { get; }

    void Initialize ();
}