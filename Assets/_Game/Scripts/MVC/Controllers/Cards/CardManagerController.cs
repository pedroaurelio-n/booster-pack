using UnityEngine;

public class CardManagerController
{
    readonly ICardManagerModel _model;
    readonly GameUIView _gameUIView;
    readonly UIViewFactory _uiViewFactory;

    ButtonUIController _spawnButtonController;
    ButtonUIController _despawnButtonController;
    
    CardUIView _cardUIView;

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
        _despawnButtonController = new ButtonUIController(gameUIView, "Despawn Card");
    }

    public void Initialize ()
    {
        AddViewListeners();
        SetupPool();
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
        ICardModel cardModel = new CardModel(_model.GetRandomCard());
        _cardUIView = GetCardUIView();

        CardController cardController = new(cardModel, _cardUIView);
        cardController.Initialize();
    }

    void DespawnCard () => _uiViewFactory.ReleaseView(nameof(CardManagerController), _cardUIView);

    CardUIView GetCardUIView ()
    {
        return _cardUIView == null || !_cardUIView.IsActive
            ? _uiViewFactory.GetView<CardUIView>(nameof(CardManagerController))
            : _cardUIView;
    }

    void AddViewListeners ()
    {
        _spawnButtonController.View.OnClick += HandleSpawnClick;
        _despawnButtonController.View.OnClick += HandleDespawnClick;
    }
    
    void RemoveViewListeners ()
    {
        _spawnButtonController.View.OnClick -= HandleSpawnClick;
        _despawnButtonController.View.OnClick -= HandleDespawnClick;
    }

    void HandleSpawnClick () => CreateNewCard();

    void HandleDespawnClick () => DespawnCard();
}