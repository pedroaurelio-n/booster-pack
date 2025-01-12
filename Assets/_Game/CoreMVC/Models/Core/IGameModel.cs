using UnityEngine;

public interface IGameModel
{
    IMouseInputModel MouseInputModel { get; }
    
    ICardManagerModel CardManagerModel { get; }
    IBoosterPackModel BoosterPackModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
}