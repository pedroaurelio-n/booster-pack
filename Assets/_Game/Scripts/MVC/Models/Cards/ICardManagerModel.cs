using System;

public interface ICardManagerModel : IDisposable
{
    ICardModel GetCardByUid (int uid);
    ICardModel GetRandomCard ();
}