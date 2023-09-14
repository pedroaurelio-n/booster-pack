using System;
using System.Linq;
using Random = UnityEngine.Random;

public class CardManagerModel : ICardManagerModel
{
    readonly ICardListSettings _settings;
    
    public CardManagerModel (ICardListSettings settings)
    {
        _settings = settings;
    }
    
    public ICardModel GetCardByUid (int uid)
    {
        ICardSettings cardSettings = _settings.Cards.FirstOrDefault(x => x.Uid == uid);
        if (cardSettings == default)
            throw new InvalidOperationException($"Couldn't find Uid {uid} in settings");

        ICardModel cardModel = new CardModel(cardSettings);
        return cardModel;
    }

    public ICardModel GetRandomCard ()
    {
        int randomUid = Random.Range(0, _settings.Cards.Count);
        return GetCardByUid(randomUid);
    }
}