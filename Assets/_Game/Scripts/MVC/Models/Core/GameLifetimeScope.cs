using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public IGameModel GameModel { get; private set; }
    public GameController GameController { get; private set; }
    
    [SerializeField] SpawnCardUIView spawnCardUIView;
    [SerializeField] CardUIView cardUIViewPrefab;
    
    SettingsManager _settingsManager;

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
        _settingsManager = new SettingsManager();
        
        GameInstaller installer = new(
            _settingsManager.CardListSettings.Instance,
            spawnCardUIView,
            cardUIViewPrefab
        );
        installer.Install(builder);
    }
}