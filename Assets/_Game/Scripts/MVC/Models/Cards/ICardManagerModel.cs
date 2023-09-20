public interface ICardManagerModel
{
    ICardModel GetCardByUid (int uid);
    ICardModel GetRandomCard ();
}