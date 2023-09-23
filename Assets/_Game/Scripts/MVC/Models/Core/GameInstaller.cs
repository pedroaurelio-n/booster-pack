using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly GameUIView _gameUIView;
    readonly MapView _mapView;
    readonly UIViewFactory _uiViewFactory;
    readonly IRandomProvider _randomProvider;
    readonly SettingsManager _settingsManager;
    
    public GameInstaller (
        GameUIView gameUIView,
        MapView mapView,
        UIViewFactory uiViewFactory,
        IRandomProvider randomProvider,
        SettingsManager settings
    )
    {
        _gameUIView = gameUIView;
        _mapView = mapView;
        _uiViewFactory = uiViewFactory;
        _randomProvider = randomProvider;
        _settingsManager = settings;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_settingsManager.CardListSettings.Instance);
        
        builder.RegisterInstance(_gameUIView);
        builder.RegisterInstance(_mapView);
        builder.RegisterInstance(_uiViewFactory);

        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);
        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        
        builder.Register<GameController>(Lifetime.Scoped);
        builder.Register<CardManagerController>(Lifetime.Scoped);
    }
}
