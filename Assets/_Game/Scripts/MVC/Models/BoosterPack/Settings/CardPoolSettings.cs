using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class CardPoolSettings : ICardPoolSettings
{
    [JsonProperty]
    public CardRarity Rarity { get; }
    
    [JsonProperty]
    public float Chance { get; }
    
    [JsonProperty]
    public IReadOnlyList<int> Ids { get; }

    [JsonConstructor]
    public CardPoolSettings (
        CardRarity rarity,
        float chance,
        int[] ids
    )
    {
        Rarity = rarity;
        Chance = chance;
        Ids = ids;
    }
}