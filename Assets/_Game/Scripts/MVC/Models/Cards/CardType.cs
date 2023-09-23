public enum CardType
{
    Monster = 0,
    Magic = 1
}

public static class CardTypeExtensions
{
    public static string ToText (this CardType cardType)
    {
        return $"{cardType.ToString()} Card";
    }
}