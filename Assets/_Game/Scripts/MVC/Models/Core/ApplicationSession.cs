using System;

public class ApplicationSession
{
    public event Action OnInitializationComplete;
    
    public GameSession GameSession { get; private set; }

    readonly LoadingManager _loadingManager;
    readonly string _startScene;

    public ApplicationSession (
        LoadingManager loadingManager,
        string startScene
    )
    {
        _loadingManager = loadingManager;
        _startScene = startScene;
    }

    public void Initialize ()
    {
        GameSession = new GameSession(_loadingManager, _startScene);
        GameSession.OnInitializationComplete += HandleInitializationComplete;
        GameSession.Initialize();
    }

    void HandleInitializationComplete ()
    {
        GameSession.OnInitializationComplete -= HandleInitializationComplete;
        OnInitializationComplete?.Invoke();
    }
}
