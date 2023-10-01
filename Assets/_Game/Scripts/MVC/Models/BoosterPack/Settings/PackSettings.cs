using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class PackSettings : IPackSettings
{
    [JsonProperty]
    public int Uid { get; }
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty]
    public int CardQuantity { get; }
    
    [JsonProperty]
    public IReadOnlyList<ICardPoolSettings> CardPools { get; }

    [JsonConstructor]
    public PackSettings (
        int uid,
        string name,
        int cardQuantity,
        CardPoolSettings[] cardPools
    )
    {
        Uid = uid;
        Name = name;
        CardQuantity = cardQuantity;
        CardPools = cardPools;
    }
}