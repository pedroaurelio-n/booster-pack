using System;

public class ApplicationSession
{
    public event Action OnInitializationComplete;
    
    public GameSession GameSession { get; private set; }

    readonly ILoadingManager _loadingManager;

    public ApplicationSession (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }

    public void Initialize (string currentScene)
    {
        GameSession = new GameSession(_loadingManager, currentScene);
        GameSession.OnInitializationComplete += HandleInitializationComplete;
        GameSession.CurrentScene = currentScene;
        GameSession.Initialize();
    }

    //TODO pedro: Maybe create a new class lower than GameSession (like GameCore) to dispose it instead of GameSession entirely
    public void ChangeScene (string newScene)
    {
        GameSession.Dispose();
        Initialize(newScene);
    }

    void HandleInitializationComplete ()
    {
        GameSession.OnInitializationComplete -= HandleInitializationComplete;
        OnInitializationComplete?.Invoke();
    }
}
