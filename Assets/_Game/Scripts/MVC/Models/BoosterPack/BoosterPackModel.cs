using System;
using System.Collections.Generic;
using System.Linq;

public class BoosterPackModel : IBoosterPackModel
{
    public IReadOnlyList<IPackSettings> Packs => _settings.Packs;
    public string CurrentPackName => _currentPack.Name;
    public int CurrentCardQuantity => _currentPack.RandomQuantity ?? _currentPack.CardSpots.Count;

    readonly IBoosterPackSettings _settings;
    readonly ICardManagerModel _cardManagerModel;
    readonly IRandomProvider _randomProvider;

    IPackSettings _currentPack;
    List<WeightedObject<CardRarity>> _currentCardPool = new();

    public BoosterPackModel (
        IBoosterPackSettings settings,
        ICardManagerModel cardManagerModel,
        IRandomProvider randomProvider
    )
    {
        _settings = settings;
        _cardManagerModel = cardManagerModel;
        _randomProvider = randomProvider;
    }
    
    public void UpdateCurrentPack (int uid)
    {
        _currentPack = _settings.Packs.First(x => x.Uid == uid);
        
        _currentCardPool.Clear();
        foreach (ICardPoolSettings cardPool in _currentPack.CardPools)
            _currentCardPool.Add(new WeightedObject<CardRarity>(cardPool.Rarity, cardPool.Chance));
    }
    
    public string GetPackNameByUid (int uid)
    {
        return _settings.Packs.First(x => x.Uid == uid).Name;
    }

    public ICardModel GenerateCard (int currentSpot)
    {
        if (_currentPack.CardSpots == null)
            return GetRandomCard();

        return GetCardForCurrentSpot(currentSpot);
    }

    ICardModel GetRandomCard ()
    {
        CardRarity rarity = _randomProvider.WeightedRandom(_currentCardPool);
        ICardModel selectedCard = GetRandomCardFromListByRarity(rarity);
        
        if (selectedCard == null)
            throw new InvalidOperationException(
                $"Could not select card with rarity of {rarity} in pack {_currentPack.Name}.");
        
        return selectedCard;
    }
    
    ICardModel GetCardForCurrentSpot (int currentIndex)
    {
        ICardSelectionSettings selectionSettings = _currentPack.CardSpots[currentIndex];
        ICardModel selectedCard;

        switch (selectionSettings.Type)
        {
            case CardSelectionType.Fixed:
                selectedCard = GetRandomCardFromListByRarity(selectionSettings.Rarity);
                break;
            case CardSelectionType.GreaterOrEqual:
                List<WeightedObject<CardRarity>> modifiedList =
                    _currentCardPool.Where(x => (int)x.Obj >= (int)selectionSettings.Rarity).ToList();
                selectedCard = GetRandomCardFromListByRarity(_randomProvider.WeightedRandom(modifiedList));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (selectedCard == null)
            throw new InvalidOperationException(
                $"Could not select card for spot {currentIndex} with rarity of {selectionSettings.Rarity} in pack {_currentPack.Name}.");
        
        return selectedCard;
    }

    ICardModel GetRandomCardFromListByRarity (CardRarity rarity)
    {
        ICardPoolSettings poolSettings = _currentPack.CardPools.First(x => x.Rarity == rarity);
        int randomIndex = _randomProvider.Range(0, poolSettings.Ids.Count);

        ICardModel selectedCard = _cardManagerModel.GetCardByUid(poolSettings.Ids[randomIndex]);
        selectedCard.AssignRarity(rarity);

        return selectedCard;
    }
}