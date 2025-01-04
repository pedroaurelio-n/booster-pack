using System;
using System.Linq;
using UnityEngine;

public class CardController
{
    const int SPACING = 5;
    
    readonly CardView _view;
    readonly CardColorsOptions _options = GameGlobalOptions.Instance.CardColors;
    
    ICardModel _model;

    public CardController (CardView view)
    {
        _view = view;
        SetViewActive(false);
    }

    public void UpdateModel (ICardModel cardModel)
    {
        _model = cardModel;
    }

    public void Initialize (int index, int cardCount)
    {
        RemoveViewListeners();
        AddViewListeners();
        
        SetViewActive(true);
        SyncView();
        SetPositionX(index, cardCount);
    }
    
    public void SetViewActive (bool value)
    {
        _view.CardAnimation.Initialize();
        _view.CardAnimation.StopRotation();
        _view.SetActiveState(value);
        
        if (value)
            _view.CardAnimation.StartRotationAndResetScale();
    }

    void SyncView ()
    {
        Sprite cardSprite = Resources.Load<Sprite>($"CardIcons/{_model.Uid}");

        _view.SetTitleText(_model.Name);
        _view.SetDescriptionText(_model.Description);
        _view.SetTypeText(_model.Type);
        _view.SetLevel(_model.Level);
        _view.SetArtSprite(cardSprite);
        _view.SetAttack(_model.Attack);
        _view.SetDefense(_model.Defense);
        SetColor();
    }

    void SetColor ()
    {
        Color selectedColor = _options.TypeColors.FirstOrDefault(x => x.Type == _model.Type).Color;
        
        if (selectedColor == default)
            throw new InvalidOperationException($"Desired type {_model.Type} doesn't have a color configured.");
        
        _view.SetColor(selectedColor);
    }

    void SetPositionX (int index, int cardCount)
    {
        float posX = ((index + 1) - (cardCount + 1) * 0.5f ) * SPACING;
        _view.transform.position = new Vector3(posX, 2.5f, 0);
    }

    void AddViewListeners ()
    {
        _view.OnEntered += HandleEntered;
        _view.OnExited += HandleExited;
    }

    void RemoveViewListeners ()
    {
        _view.OnEntered -= HandleEntered;
        _view.OnExited -= HandleExited;
    }

    void HandleEntered ()
    {
        Color selectedColor = _options.RarityColors.FirstOrDefault(x => x.Rarity == _model.CurrentRarity).Color;

        if (selectedColor == default)
            throw new InvalidOperationException(
                $"Desired rarity {_model.CurrentRarity} doesn't have a color configured."
            );

        ParticleSystem.MainModule main = _view.RarityParticles.main;
        main.startColor = selectedColor;
        _view.RarityParticles.Play();
        
        _view.CardAnimation.ResetRotationAndZoomIn();
    }

    void HandleExited ()
    {
        _view.RarityParticles.Stop();
        _view.CardAnimation.StartRotationAndResetScale();
    }
}