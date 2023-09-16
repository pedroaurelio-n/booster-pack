public interface IGameModel
{
    ICardManagerModel CardManagerModel { get; }

    void Initialize ();
}