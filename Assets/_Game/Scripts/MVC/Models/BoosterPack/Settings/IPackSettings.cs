using System.Collections.Generic;

public interface IPackSettings
{
    string Name { get; }
    IReadOnlyList<ICardPoolSettings> CardPools { get; }
}