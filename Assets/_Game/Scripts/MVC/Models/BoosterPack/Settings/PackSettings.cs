using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class PackSettings : IPackSettings
{
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty]
    public IReadOnlyList<ICardPoolSettings> CardPools { get; }

    [JsonConstructor]
    public PackSettings (
        string name,
        CardPoolSettings[] cardPools
    )
    {
        Name = name;
        CardPools = cardPools;
    }
}