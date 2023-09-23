using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class BoosterPackSettings : IBoosterPackSettings
{
    [JsonProperty]
    public IReadOnlyList<IPackSettings> Packs { get; }

    [JsonConstructor]
    public BoosterPackSettings (PackSettings[] packs)
    {
        Packs = packs;
    }
}