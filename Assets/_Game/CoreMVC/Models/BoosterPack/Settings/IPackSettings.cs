using System.Collections.Generic;

public interface IPackSettings
{
    int Uid { get; }
    string Name { get; }
    int? RandomQuantity { get; }
    IReadOnlyList<ICardSelectionSettings> CardSpots { get; }
    IReadOnlyList<ICardPoolSettings> CardPools { get; }
}