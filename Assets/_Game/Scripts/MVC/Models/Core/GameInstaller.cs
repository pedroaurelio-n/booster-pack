using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly LoadingManager _loadingManager;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly GameUIView _gameUIView;
    readonly SceneView _sceneView;
    readonly UIViewFactory _uiViewFactory;
    readonly SettingsManager _settingsManager;
    readonly IRandomProvider _randomProvider;
    readonly IPhysicsProvider _physicsProvider;
    
    public GameInstaller (
        LoadingManager loadingManager,
        IGameSessionInfoProvider gameSessionInfoProvider,
        GameUIView gameUIView,
        SceneView sceneView,
        UIViewFactory uiViewFactory,
        SettingsManager settings,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider
    )
    {
        _loadingManager = loadingManager;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _gameUIView = gameUIView;
        _sceneView = sceneView;
        _uiViewFactory = uiViewFactory;
        _settingsManager = settings;
        _randomProvider = randomProvider;
        _physicsProvider = physicsProvider;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_settingsManager.CardListSettings.Instance);
        builder.RegisterInstance(_settingsManager.BoosterPackSettings.Instance);
        
        builder.RegisterInstance(_loadingManager);
        builder.RegisterInstance(_gameSessionInfoProvider);
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_physicsProvider);
        
        builder.RegisterInstance(_gameUIView);
        builder.RegisterInstance(_sceneView);
        builder.RegisterInstance(_sceneView.MouseInput);
        builder.RegisterInstance(_uiViewFactory);

        builder.Register<IMouseInputModel, MouseInputModel>(Lifetime.Scoped);

        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        builder.Register<IBoosterPackModel, BoosterPackModel>(Lifetime.Scoped);
        builder.Register<ISceneChangerModel, SceneChangerModel>(Lifetime.Scoped);
        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);

        builder.Register<MouseInputController>(Lifetime.Scoped);
        
        builder.Register<BoosterPackManagerUIController>(Lifetime.Scoped);
        builder.Register<SceneChangerUIController>(Lifetime.Scoped);
        builder.Register<GameController>(Lifetime.Scoped);
    }
}
