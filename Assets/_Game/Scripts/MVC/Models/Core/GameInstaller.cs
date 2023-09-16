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

        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);
        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        
        builder.Register<GameController>(Lifetime.Scoped);
        builder.Register<CardManagerController>(Lifetime.Scoped);

        builder.RegisterComponentInNewPrefab(_cardUIViewPrefab, Lifetime.Scoped);
        builder.RegisterComponent(_spawnCardUIView);
    }
}
