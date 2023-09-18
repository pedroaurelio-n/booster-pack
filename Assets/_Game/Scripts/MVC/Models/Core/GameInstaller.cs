using VContainer;
using VContainer.Unity;

public class GameInstaller : IInstaller
{
    readonly GameUIView _gameUIView;
    readonly IRandomProvider _randomProvider;
    readonly SettingsManager _settingsManager;
    
    public GameInstaller (
        GameUIView gameUIView,
        IRandomProvider randomProvider,
        SettingsManager settings
    )
    {
        _gameUIView = gameUIView;
        _randomProvider = randomProvider;
        _settingsManager = settings;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_randomProvider);
        builder.RegisterInstance(_settingsManager.CardListSettings.Instance);
        builder.RegisterInstance(_gameUIView);

        builder.Register<IGameModel, GameModel>(Lifetime.Scoped);
        builder.Register<ICardManagerModel, CardManagerModel>(Lifetime.Scoped);
        
        builder.Register<GameController>(Lifetime.Scoped);
        builder.Register<CardManagerController>(Lifetime.Scoped);
    }
}
