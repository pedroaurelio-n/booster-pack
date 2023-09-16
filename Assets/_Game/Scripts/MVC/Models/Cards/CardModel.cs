public class CardModel : ICardModel
{
    public int Uid => _settings.Uid;
    public CardType Type => _settings.Type;
    public string Name => _settings.Name;
    public string Description => _settings.Description;
    public int? Attack => _settings.Attack;
    public int? Defense => _settings.Defense;

    readonly ICardSettings _settings;

    public CardModel (ICardSettings settings)
    {
        _settings = settings;
    }
}