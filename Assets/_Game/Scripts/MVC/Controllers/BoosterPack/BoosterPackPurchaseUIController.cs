using System;
using UnityEngine;

public class BoosterPackPurchaseUIController
{
    public event Action<int> OnBoosterPackPurchase;
    public int Uid { get; }
    public BoosterPackPurchaseUIView View { get; private set; }
    
    readonly GameUIView _gameUIView;
    
    public BoosterPackPurchaseUIController (GameUIView gameUIView, int uid, string name)
    {
        Uid = uid;
        _gameUIView = gameUIView;
        InstantiateSpawnButton();
        View.SetPackName(name);
    }

    public void Initialize ()
    {
        AddViewListeners();
    }

    void InstantiateSpawnButton () 
        => View = GameObject.Instantiate(
            Resources.Load<BoosterPackPurchaseUIView>("BoosterPackPurchaseUIView"),
            _gameUIView.ButtonContainer
        );

    void AddViewListeners ()
    {
        View.OnClick += HandleViewClick;
    }
    
    void RemoveViewListeners ()
    {
        View.OnClick -= HandleViewClick;
    }

    void HandleViewClick () => OnBoosterPackPurchase?.Invoke(Uid);
}