using System;
using System.Linq;
using Random = UnityEngine.Random;

public class CardManagerModel : ICardManagerModel
{
    readonly ICardListSettings _settings;
    readonly IRandomProvider _randomProvider;
    
    public CardManagerModel (
        ICardListSettings settings,
        IRandomProvider randomProvider
    )
    {
        _settings = settings;
        _randomProvider = randomProvider;
    }
    
    public ICardSettings GetCardByUid (int uid)
    {
        ICardSettings cardSettings = _settings.Cards.FirstOrDefault(x => x.Uid == uid);
        if (cardSettings == default)
            throw new InvalidOperationException($"Couldn't find Uid {uid} in settings");

        return cardSettings;
    }

    public ICardSettings GetRandomCard ()
    {
        int randomUid = _randomProvider.Range(0, _settings.Cards.Count);
        return GetCardByUid(randomUid);
    }
}