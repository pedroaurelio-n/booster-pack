using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class GameSession : IGameSessionInfoProvider, IDisposable
{
    public event Action OnInitializationComplete;

    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    public GameUIView GameUIView { get; private set; }
    
    //TODO pedro: Separate Game scope from Map scope
    public SceneView SceneView { get; private set; }
    
    public string CurrentScene { get; set; }

    public int CurrentSceneIndex => SceneManager.GetSceneByName(CurrentScene).buildIndex;

    GameLifetimeScope MainScope => GameLifetimeScope.Instance;

    readonly ILoadingManager _loadingManager;

    LifetimeScope _gameScope;
    
    SettingsManager _settingsManager;
    IRandomProvider _randomProvider;
    IPhysicsProvider _physicsProvider;
    
    public GameSession (
        ILoadingManager loadingManager,
        string startScene
    )
    {
        _loadingManager = loadingManager;
        CurrentScene = startScene;
    }

    public void Initialize ()
    {
        CreateProviders();
        _gameScope = CreateGameScope();

        GameModel = _gameScope.Container.Resolve<IGameModel>();
        GameModel.Initialize();
    
        GameController = _gameScope.Container.Resolve<GameController>();
        GameController.Initialize();

        GameUIView.FadeToBlackManager.FadeOut(null);
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
        
        SceneView = Object.Instantiate(Resources.Load<SceneView>($"{CurrentScene}View"));
        SceneView.Initialize();
    
        UIViewFactory uiViewFactory = new();
        
        GameInstaller installer = new(
            _loadingManager,
            this,
            GameUIView,
            SceneView,
            uiViewFactory,
            _settingsManager,
            _randomProvider,
            _physicsProvider
        );
        
        return MainScope.CreateChild(installer);
    }

    public void Dispose ()
    {
        _gameScope.Dispose();
    }
}
