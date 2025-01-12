public class GameController
{
    public MouseInputController MouseInputController { get; private set; }
    public BoosterPackManagerUIController BoosterPackManagerUIController { get; private set; }
    public SceneChangerUIController SceneChangerUIController { get; private set; }

    public GameController (
        MouseInputController mouseInputController,
        BoosterPackManagerUIController boosterPackManagerUIController,
        SceneChangerUIController sceneChangerUIController
    )
    {
        MouseInputController = mouseInputController;
        BoosterPackManagerUIController = boosterPackManagerUIController;
        SceneChangerUIController = sceneChangerUIController;
    }

    public void Initialize ()
    {
        MouseInputController.Initialize();
        BoosterPackManagerUIController.Initialize();
        SceneChangerUIController.Initialize();
    }
}