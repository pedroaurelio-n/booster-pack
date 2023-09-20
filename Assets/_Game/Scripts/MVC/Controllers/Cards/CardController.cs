public class CardController
{
    readonly CardUIView _view;
    
    ICardModel _model;

    public CardController (CardUIView view)
    {
        _view = view;
        SetViewActive(false);
    }

    public void UpdateModel (ICardModel cardModel)
    {
        _model = cardModel;
    }

    public void Initialize ()
    {
        SetViewActive(true);
        SyncView();
    }
    
    public void SetViewActive (bool value) => _view.gameObject.SetActive(value);

    void SyncView ()
    {
        _view.SetTitleText(_model.Name);
        _view.SetBackgroundColor(_model.Type == CardType.Monster ? _view.Colors.MonsterColor : _view.Colors.MagicColor);
    }
}