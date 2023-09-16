public interface ICardModel
{
    int Uid { get; }
    CardType Type { get; }
    string Name { get; }
    string Description { get; }
    int? Attack { get; }
    int? Defense { get; }
}