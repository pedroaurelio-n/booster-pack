public enum CardType
{
    Monster,
    Magic
}

public static class CardTypeExtensions
{
    public static string ToText (this CardType cardType)
    {
        return $"{cardType.ToString()} Card";
    }
}