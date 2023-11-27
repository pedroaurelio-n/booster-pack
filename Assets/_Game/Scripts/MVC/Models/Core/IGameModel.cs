using UnityEngine;

public interface IGameModel
{
    IMouseInputModel MouseInputModel { get; }
    
    ICardManagerModel CardManagerModel { get; }
    IBoosterPackModel BoosterPackModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
}