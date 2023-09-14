using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly ICardListSettings _settings;
    readonly SpawnCardUIView _spawnCardUIView;
    readonly CardUIView _cardUIViewPrefab;
    
    public GameInstaller (
        ICardListSettings settings,
        SpawnCardUIView spawnCardUIView,
        CardUIView cardUIViewPrefab
    )
    {
        _settings = settings;
        _spawnCardUIView = spawnCardUIView;
        _cardUIViewPrefab = cardUIViewPrefab;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_settings);

        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        builder.Register<ICardModel, CardModel>(Lifetime.Transient);

        builder.RegisterComponentInNewPrefab(_cardUIViewPrefab, Lifetime.Transient);
        builder.RegisterComponent(_spawnCardUIView);
        
        builder.RegisterEntryPoint<CardManagerController>(Lifetime.Scoped);
        builder.RegisterEntryPoint<CardController>(Lifetime.Transient);
    }
}
