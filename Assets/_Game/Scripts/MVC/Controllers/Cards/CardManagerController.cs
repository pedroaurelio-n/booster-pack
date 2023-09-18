using UnityEngine;

public class CardManagerController
{
    readonly ICardManagerModel _model;
    readonly GameUIView _gameUIView;

    SpawnCardController _spawnCardController;
    CardUIView _cardUIView;

    public CardManagerController (
        ICardManagerModel model,
        GameUIView gameUIView
    )
    {
        _model = model;
        _gameUIView = gameUIView;

        _spawnCardController = new SpawnCardController(gameUIView);
    }

    public void Initialize ()
    {
        AddViewListeners();
    }

    void CreateNewCard ()
    {
        ICardModel cardModel = new CardModel(_model.GetRandomCard());
        _cardUIView = GetCardUIView();

        CardController cardController = new(cardModel, _cardUIView);
        cardController.Initialize();
    }

    CardUIView GetCardUIView ()
    {
        return _cardUIView != null
            ? _cardUIView
            : GameObject.Instantiate(Resources.Load<CardUIView>("CardUIView"), _gameUIView.CardContainer);
    }

    void AddViewListeners ()
    {
        _spawnCardController.View.OnClick += HandleSpawnClick;
    }
    
    void RemoveViewListeners ()
    {
        _spawnCardController.View.OnClick -= HandleSpawnClick;
    }

    void HandleSpawnClick () => CreateNewCard();
}