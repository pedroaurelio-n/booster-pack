using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class CardListSettings : ICardListSettings
{
    [JsonProperty]
    public IReadOnlyList<ICardSettings> Cards { get; }

    public CardListSettings (CardSettings[] cards)
    {
        Cards = cards;
    }
}