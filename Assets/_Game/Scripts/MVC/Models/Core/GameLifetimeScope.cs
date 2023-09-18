using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    
    public SettingsManager SettingsManager { get; private set; }
    public IRandomProvider RandomProvider { get; private set; }
    
    [SerializeField] SpawnCardUIView spawnCardUIView;
    [SerializeField] CardUIView cardUIViewPrefab;

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
    }

    void CreateInstaller (IContainerBuilder builder)
    {
        GameInstaller installer = new(
            RandomProvider,
            SettingsManager,
            spawnCardUIView,
            cardUIViewPrefab
        );
        installer.Install(builder);
    }
}