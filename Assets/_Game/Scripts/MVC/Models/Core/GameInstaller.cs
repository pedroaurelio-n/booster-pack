using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly IRandomProvider _randomProvider;
    readonly SettingsManager _settingsManager;
    readonly SpawnCardUIView _spawnCardUIView;
    readonly CardUIView _cardUIViewPrefab;
    
    public GameInstaller (
        IRandomProvider randomProvider,
        SettingsManager settings,
        SpawnCardUIView spawnCardUIView,
        CardUIView cardUIViewPrefab
    )
    {
        _randomProvider = randomProvider;
        _settingsManager = settings;
        _spawnCardUIView = spawnCardUIView;
        _cardUIViewPrefab = cardUIViewPrefab;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_settingsManager.CardListSettings.Instance);

        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);
        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        
        builder.Register<GameController>(Lifetime.Scoped);
        builder.Register<CardManagerController>(Lifetime.Scoped);

        builder.RegisterComponentInNewPrefab(_cardUIViewPrefab, Lifetime.Scoped);
        builder.RegisterComponent(_spawnCardUIView);
    }
}
