using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class GameSession : IDisposable
{
    public event Action OnInitializationComplete;

    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    public GameUIView GameUIView { get; private set; }
    
    //TODO pedro: Separate Game scope from Map scope
    public MapView MapView { get; private set; }
    
    GameLifetimeScope MainScope => GameLifetimeScope.Instance;

    readonly LoadingManager _loadingManager;

    LifetimeScope gameScope;
    
    SettingsManager _settingsManager;
    IRandomProvider _randomProvider;
    IPhysicsProvider _physicsProvider;
    
    string _currentScene;

    public GameSession (
        LoadingManager loadingManager,
        string startScene
    )
    {
        _loadingManager = loadingManager;
        _currentScene = startScene;
    }

    public void LoadScene (string newScene) => _currentScene = newScene;
    
    public void Initialize ()
    {
        CreateProviders();
        gameScope = CreateGameScope();

        GameModel = gameScope.Container.Resolve<IGameModel>();
        GameModel.Initialize();
    
        GameController = gameScope.Container.Resolve<GameController>();
        GameController.Initialize();

        OnInitializationComplete?.Invoke();
    }
    
    void CreateProviders ()
    {
        _settingsManager = new SettingsManager();
        _randomProvider = new RandomProvider();
        _physicsProvider = new PhysicsProvider();
    }

    LifetimeScope CreateGameScope ()
    {
        GameUIView = Object.Instantiate(Resources.Load<GameUIView>("GameUIView"));
        
        MapView = Object.Instantiate(Resources.Load<MapView>($"{_currentScene}View"));
        MapView.Initialize();
    
        UIViewFactory uiViewFactory = new();
        
        GameInstaller installer = new(
            _loadingManager,
            GameUIView,
            MapView,
            uiViewFactory,
            _settingsManager,
            _randomProvider,
            _physicsProvider
        );
        
        return MainScope.CreateChild(installer);
    }

    public void Dispose ()
    {
        Debug.Log($"DISPOSE GAME SESSION");
        gameScope.Dispose();
    }
}
