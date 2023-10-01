using System.Collections.Generic;

public interface IPackSettings
{
    int Uid { get; }
    string Name { get; }
    int CardQuantity { get; }
    IReadOnlyList<ICardPoolSettings> CardPools { get; }
}