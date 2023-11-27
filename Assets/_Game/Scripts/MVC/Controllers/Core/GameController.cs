public class GameController
{
    public MouseInputController MouseInputController { get; private set; }
    public BoosterPackManagerController BoosterPackManagerController { get; private set; }

    public GameController (
        MouseInputController mouseInputController,
        BoosterPackManagerController boosterPackManagerController
    )
    {
        MouseInputController = mouseInputController;
        BoosterPackManagerController = boosterPackManagerController;
    }

    public void Initialize ()
    {
        MouseInputController.Initialize();
        BoosterPackManagerController.Initialize();
    }
}