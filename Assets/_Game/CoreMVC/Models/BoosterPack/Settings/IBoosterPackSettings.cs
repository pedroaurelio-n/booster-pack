using System.Collections.Generic;

public interface IBoosterPackSettings
{
    IReadOnlyList<IPackSettings> Packs { get; }
}