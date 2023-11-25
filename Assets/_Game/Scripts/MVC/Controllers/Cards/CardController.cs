using UnityEngine;

public class CardController
{
    const int SPACING = 5;
    
    readonly CardView _view;
    
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
        SetViewActive(true);
        SyncView();
        SetPositionX(index, cardCount);
    }
    
    public void SetViewActive (bool value)
    {
        _view.CardAnimation.StopRotation();
        _view.SetActiveState(value);
        
        if (value)
            _view.CardAnimation.StartRotation();
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
        _view.SetColor(_model.Type == CardType.Monster ? _view.Colors.MonsterColor : _view.Colors.MagicColor);
    }

    void SetPositionX (int index, int cardCount)
    {
        float posX = (index - (cardCount + 1) * 0.5f ) * SPACING;
        _view.transform.position = new Vector3(posX, 2.5f, 0);
    }
}