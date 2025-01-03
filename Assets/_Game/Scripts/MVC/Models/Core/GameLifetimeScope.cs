using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    public GameUIView GameUIView { get; private set; }
    
    //TODO pedro: Separate Game scope from Map scope
    public MapView MapView { get; private set; }
    
    public SettingsManager SettingsManager { get; private set; }
    public IRandomProvider RandomProvider { get; private set; }
    public IPhysicsProvider PhysicsProvider { get; private set; }

    protected override void Awake ()
    {
        base.Awake();
        Initialize();
    }

    void Initialize ()
    {
        GameModel = Container.Resolve<IGameModel>();
        GameModel.Initialize();

        GameController = Container.Resolve<GameController>();
        GameController.Initialize();
    }

    protected override void Configure (IContainerBuilder builder)
    {
        CreateProviders();
        CreateInstaller(builder);
    }

    void CreateProviders ()
    {
        SettingsManager = new SettingsManager();
        RandomProvider = new RandomProvider();
        PhysicsProvider = new PhysicsProvider();
    }

    void CreateInstaller (IContainerBuilder builder)
    {
        GameUIView = GameObject.Instantiate(Resources.Load<GameUIView>("GameUIView"));
        
        MapView = GameObject.Instantiate(Resources.Load<MapView>("MapView"));
        MapView.Initialize();

        UIViewFactory uiViewFactory = new();
        
        GameInstaller installer = new(
            GameUIView,
            MapView,
            uiViewFactory,
            SettingsManager,
            RandomProvider,
            PhysicsProvider
        );
        installer.Install(builder);
    }
}