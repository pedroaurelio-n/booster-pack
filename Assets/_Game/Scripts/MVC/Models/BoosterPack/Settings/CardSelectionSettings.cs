using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class CardSelectionSettings : ICardSelectionSettings
{
    [JsonProperty]
    public CardSelectionType Type { get; }
    
    [JsonProperty]
    public CardRarity Rarity { get; }
    
    [JsonConstructor]
    public CardSelectionSettings (
        CardSelectionType type,
        CardRarity rarity
    )
    {
        Type = type;
        Rarity = rarity;
    }
}