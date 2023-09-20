using System.Collections.Generic;
using UnityEngine;

public class CardManagerController
{
    const int MIN_CARD_COUNT = 1;
    const int MAX_CARD_COUNT = 5;
    
    readonly ICardManagerModel _model;
    readonly GameUIView _gameUIView;
    readonly UIViewFactory _uiViewFactory;

    ButtonUIController _spawnButtonController;

    List<CardController> _cardControllers;

    public CardManagerController (
        ICardManagerModel model,
        GameUIView gameUIView,
        UIViewFactory uiViewFactory
    )
    {
        _model = model;
        _gameUIView = gameUIView;
        _uiViewFactory = uiViewFactory;

        _spawnButtonController = new ButtonUIController(gameUIView, "Spawn Card");
    }

    public void Initialize ()
    {
        AddViewListeners();
        SetupPool();
    }

    void SyncView ()
    {
        int cardCount = Random.Range(MIN_CARD_COUNT, MAX_CARD_COUNT + 1);
        CreateMissingInstances(cardCount);
        UpdateInstances(cardCount);
    }

    void SetupPool ()
    {
        _uiViewFactory.SetupPool(
            nameof(CardManagerController),
            Resources.Load<CardUIView>("CardUIView"),
            _gameUIView.CardContainer
        );
    }

    void CreateNewCard ()
    {
        SyncView();
    }

    void CreateMissingInstances (int target)
    {
        _cardControllers ??= new List<CardController>();
        int missingCount = target - _cardControllers.Count;

        for (int i = 0; i < missingCount; i++)
        {
            CardController controller = new(_uiViewFactory.GetView<CardUIView>(nameof(CardManagerController)));
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
            controller.UpdateModel(_model.GetRandomCard());
            controller.Initialize();
        }
    }

    void AddViewListeners ()
    {
        _spawnButtonController.View.OnClick += HandleSpawnClick;
    }
    
    void RemoveViewListeners ()
    {
        _spawnButtonController.View.OnClick -= HandleSpawnClick;
    }

    void HandleSpawnClick () => CreateNewCard();
}