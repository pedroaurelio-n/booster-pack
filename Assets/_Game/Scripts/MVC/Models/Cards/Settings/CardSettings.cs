using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class CardSettings : ICardSettings
{
    [JsonProperty]
    public int Uid { get; }
    
    [JsonProperty]
    public CardType Type { get; }
    
    [JsonProperty]
    public string Name { get; }
    
    [JsonProperty]
    public string Description { get; }
    
    [JsonProperty]
    public int? Attack { get; }
    
    [JsonProperty]
    public int? Defense { get; }

    public CardSettings (
        int uid,
        CardType type,
        string name,
        string description,
        int? attack,
        int? defense
    )
    {
        Uid = uid;
        Type = type;
        Name = name;
        Description = description;
        Attack = attack;
        Defense = defense;
    }

    public override string ToString ()
    {
        return $"Name: {Name}\n"
               + $"Description: {Description}";
    }
}