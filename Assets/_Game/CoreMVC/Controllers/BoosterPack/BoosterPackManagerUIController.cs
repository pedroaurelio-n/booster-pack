using System.Collections.Generic;
using UnityEngine;

public class BoosterPackManagerUIController
{
    readonly IBoosterPackModel _model;
    readonly GameUIView _gameUIView;
    readonly SceneView _sceneView;
    readonly UIViewFactory _uiViewFactory;

    List<BoosterPackPurchaseUIController> _packButtonControllers = new();
    List<CardController> _cardControllers;

    public BoosterPackManagerUIController (
        IBoosterPackModel model,
        GameUIView gameUIView,
        SceneView sceneView,
        UIViewFactory uiViewFactory
    )
    {
        _model = model;
        _gameUIView = gameUIView;
        _sceneView = sceneView;
        _uiViewFactory = uiViewFactory;
        
        SetupUIButtons();
    }

    public void Initialize ()
    {
        AddViewListeners();
        SetupPool();
    }
    
    void SetupUIButtons ()
    {
        foreach (IPackSettings pack in _model.Packs)
        {
            BoosterPackPurchaseUIController boosterPackPurchaseController = new(_gameUIView, pack.Uid, pack.Name);
            boosterPackPurchaseController.Initialize();
            _packButtonControllers.Add(boosterPackPurchaseController);
        }
    }

    void SyncView ()
    {
        int cardCount = _model.CurrentCardQuantity;
        CreateMissingInstances(cardCount);
        UpdateInstances(cardCount);
    }

    void SetupPool ()
    {
        _uiViewFactory.SetupPool(
            nameof(CardView),
            Resources.Load<CardView>("CardView"),
            _sceneView.CardContainer
        );
    }

    void PurchaseBoosterPack (int uid)
    {
        _model.UpdateCurrentPack(uid);
        SyncView();
    }

    void CreateMissingInstances (int target)
    {
        _cardControllers ??= new List<CardController>();
        int missingCount = target - _cardControllers.Count;

        for (int i = 0; i < missingCount; i++)
        {
            CardController controller = new(_uiViewFactory.GetView<CardView>(nameof(CardView)));
            _cardControllers.Add(controller);
        }
    }

    void UpdateInstances (int target)
    {
        if (_cardControllers == null)
            return;
        
        for (int i = 0; i < _cardControllers.Count; i++)
        {
            CardController controller = _cardControllers[i];
            bool isActive = i < target;
            if (!isActive)
            {
                controller.SetViewActive(false);
                continue;
            }
            controller.UpdateModel(_model.GenerateCard(i));
            controller.Initialize(i, target);
        }
    }

    void AddViewListeners ()
    {
        foreach (BoosterPackPurchaseUIController packButtonController in _packButtonControllers)
            packButtonController.OnBoosterPackPurchase += HandleBoosterPackPurchase;
    }
    
    void RemoveViewListeners ()
    {
        foreach (BoosterPackPurchaseUIController packButtonController in _packButtonControllers)
            packButtonController.OnBoosterPackPurchase -= HandleBoosterPackPurchase;
    }

    void HandleBoosterPackPurchase (int uid) => PurchaseBoosterPack(uid);
}