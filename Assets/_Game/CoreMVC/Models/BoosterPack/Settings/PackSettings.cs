using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class PackSettings : IPackSettings
{
    [JsonProperty]
    public int Uid { get; }
    
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty("cardQuantity")]
    public int? RandomQuantity { get; }
    
    [JsonProperty]
    public IReadOnlyList<ICardSelectionSettings> CardSpots { get; }

    [JsonProperty]
    public IReadOnlyList<ICardPoolSettings> CardPools { get; }

    [JsonConstructor]
    public PackSettings (
        int uid,
        string name,
        int? cardQuantity,
        CardSelectionSettings[] cardSpots,
        CardPoolSettings[] cardPools
    )
    {
        Uid = uid;
        Name = name;
        RandomQuantity = cardQuantity;
        CardSpots = cardSpots;
        CardPools = cardPools;
    }
}