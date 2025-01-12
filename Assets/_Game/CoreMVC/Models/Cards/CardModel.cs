public class CardModel : ICardModel
{
    public int Uid => _settings.Uid;
    public CardType Type => _settings.Type;
    public CardRarity CurrentRarity { get; private set; }
    public string Name => _settings.Name;
    public string Description => _settings.Description;
    public int? Level => _settings.Level;
    public int? Attack => _settings.Attack;
    public int? Defense => _settings.Defense;

    readonly ICardSettings _settings;

    public CardModel (ICardSettings settings)
    {
        _settings = settings;
    }

    public void AssignRarity (CardRarity rarity) => CurrentRarity = rarity;
}