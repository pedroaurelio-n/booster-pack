using System.Collections.Generic;
using System.Linq;

public class BoosterPackModel : IBoosterPackModel
{
    public IReadOnlyList<IPackSettings> Packs => _settings.Packs;
    public string CurrentPackName => _currentPack.Name;
    public int CurrentCardQuantity => _currentPack.CardQuantity;

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

    public ICardModel GetCardFromPool ()
    {
        CardRarity rarity = _randomProvider.WeightedRandom(_currentCardPool);
        ICardPoolSettings poolSettings = _currentPack.CardPools.First(x => x.Rarity == rarity);
        int randomIndex = _randomProvider.Range(0, poolSettings.Ids.Count);
        return _cardManagerModel.GetCardByUid(poolSettings.Ids[randomIndex]);
    }
}