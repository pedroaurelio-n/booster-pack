using System.Collections.Generic;

public interface ICardListSettings
{
    IReadOnlyList<ICardSettings> Cards { get; }
}