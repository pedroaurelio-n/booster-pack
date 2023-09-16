public interface ICardManagerModel
{
    ICardSettings GetCardByUid (int uid);
    ICardSettings GetRandomCard ();
}