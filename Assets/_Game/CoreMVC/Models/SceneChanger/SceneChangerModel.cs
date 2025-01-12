public class SceneChangerModel : ISceneChangerModel
{
    readonly LoadingManager _loadingManager;

    public SceneChangerModel (LoadingManager loadingManager)
    {
        _loadingManager = loadingManager;
    }
    
    public void ChangeScene (string newScene)
    {
        _loadingManager.LoadNewScene(newScene);
    }
}