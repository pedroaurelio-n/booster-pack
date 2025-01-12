using System.Collections.Generic;

public interface ICardPoolSettings
{
    CardRarity Rarity { get; }
    float Chance { get; }
    IReadOnlyList<int> Ids { get; }
}