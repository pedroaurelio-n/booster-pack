public class SceneChangerModel : ISceneChangerModel
{
    readonly ILoadingManager _loadingManager;

    public SceneChangerModel (ILoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }
    
    public void ChangeScene (string newScene)
    {
        _loadingManager.LoadNewScene(newScene);
    }
}