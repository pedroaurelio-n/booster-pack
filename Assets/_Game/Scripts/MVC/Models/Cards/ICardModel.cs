public interface ICardModel
{
    int Uid { get; }
    CardType Type { get; }
    CardRarity CurrentRarity { get; }
    string Name { get; }
    string Description { get; }
    int? Level { get; }
    int? Attack { get; }
    int? Defense { get; }

    //TODO pedro: Sketchy
    void AssignRarity (CardRarity rarity);
}