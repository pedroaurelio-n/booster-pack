public class CardController
{
    readonly ICardModel _model;
    readonly CardUIView _view;

    public CardController (
        ICardModel model,
        CardUIView view
    )
    {
        _model = model;
        _view = view;
    }

    public void Initialize ()
    {
        SyncView();
    }

    void SyncView ()
    {
        _view.SetTitleText(_model.Name);
        _view.SetBackgroundColor(_model.Type == CardType.Monster ? _view.Colors.MonsterColor : _view.Colors.MagicColor);
    }
}