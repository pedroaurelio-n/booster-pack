using UnityEngine;

public class GameModel : IGameModel
{
    public IMouseInputModel MouseInputModel { get; }
    
    public ICardManagerModel CardManagerModel { get; }
    public IBoosterPackModel BoosterPackModel { get; }
    public ISceneChangerModel SceneChangerModel { get; }
    
    public Camera MainCamera { get; private set; }

    public GameModel (
        IMouseInputModel mouseInputModel,
        ICardManagerModel cardManagerModel, 
        IBoosterPackModel boosterPackModel,
        ISceneChangerModel sceneChangerModel
    )
    {
        MouseInputModel = mouseInputModel;
        CardManagerModel = cardManagerModel;
        BoosterPackModel = boosterPackModel;
        SceneChangerModel = sceneChangerModel;
    }

    public void Initialize ()
    {
        MainCamera = Camera.main;
        MouseInputModel.SetMainCamera(MainCamera);
    }
}