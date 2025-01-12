public interface ICardSettings
{
    int Uid { get; }
    CardType Type { get; }
    string Name { get; }
    string Description { get; }
    int? Level { get; }
    int? Attack { get; }
    int? Defense { get; }
}