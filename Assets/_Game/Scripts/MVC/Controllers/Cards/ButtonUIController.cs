using UnityEngine;

public class ButtonUIController
{
    public ButtonUIView View { get; private set; }
    
    readonly GameUIView _gameUIView;
    
    public ButtonUIController (GameUIView gameUIView, string text)
    {
        _gameUIView = gameUIView;
        InstantiateSpawnButton();
        View.SetText(text);
    }

    void InstantiateSpawnButton () => View =
        GameObject.Instantiate(Resources.Load<ButtonUIView>("ButtonUIView"), _gameUIView.ButtonContainer);
}