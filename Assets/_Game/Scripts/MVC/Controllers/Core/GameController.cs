public class GameController
{
    public BoosterPackManagerController BoosterPackManagerController { get; private set; }

    public GameController (BoosterPackManagerController boosterPackManagerController)
    {
        BoosterPackManagerController = boosterPackManagerController;
    }

    public void Initialize ()
    {
        BoosterPackManagerController.Initialize();
    }
}