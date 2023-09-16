public class GameController
{
    public CardManagerController CardManagerController { get; private set; }

    public GameController (CardManagerController cardManagerController)
    {
        CardManagerController = cardManagerController;
    }

    public void Initialize ()
    {
        CardManagerController.Initialize();
    }
}