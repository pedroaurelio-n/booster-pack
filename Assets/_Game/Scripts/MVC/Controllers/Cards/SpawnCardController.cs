using UnityEngine;

public class SpawnCardController
{
    public SpawnCardUIView View { get; private set; }
    
    readonly GameUIView _gameUIView;
    
    public SpawnCardController (GameUIView gameUIView)
    {
        _gameUIView = gameUIView;
        InstantiateSpawnButton();
    }

    void InstantiateSpawnButton () => View =
        GameObject.Instantiate(Resources.Load<SpawnCardUIView>("SpawnCardUIView"), _gameUIView.ButtonContainer);
}