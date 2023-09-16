using System;
using UnityEngine;
using VContainer.Unity;

public class CardManagerController
{
    readonly ICardManagerModel _model;
    readonly SpawnCardUIView _view;
    readonly CardUIView _cardUIViewPrefab;

    CardUIView _cardUIView;

    public CardManagerController (
        ICardManagerModel model,
        SpawnCardUIView view,
        CardUIView cardUIViewPrefab
    )
    {
        _model = model;
        _view = view;
        _cardUIViewPrefab = cardUIViewPrefab;
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
            : GameObject.Instantiate(_cardUIViewPrefab, _view.transform);
    }

    void AddViewListeners ()
    {
        _view.OnClick += HandleSpawnClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnClick -= HandleSpawnClick;
    }

    void HandleSpawnClick () => CreateNewCard();
}