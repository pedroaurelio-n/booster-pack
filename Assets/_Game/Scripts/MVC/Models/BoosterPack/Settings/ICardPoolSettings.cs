using System.Collections.Generic;

public interface ICardPoolSettings
{
    CardRarity Rarity { get; }
    IReadOnlyList<int> Ids { get; }
}