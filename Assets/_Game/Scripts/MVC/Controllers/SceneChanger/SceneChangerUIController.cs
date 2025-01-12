using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerUIController
{
    readonly ISceneChangerModel _model;
    readonly GameUIView _gameUIView;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    SceneChangerUIView _view;

    public SceneChangerUIController (
        ISceneChangerModel model,
        GameUIView gameUIView,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _model = model;
        _gameUIView = gameUIView;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        InstantiateSceneChangeButton();
    }

    public void Initialize ()
    {
        AddViewListeners();
    }

    void InstantiateSceneChangeButton () 
        => _view = GameObject.Instantiate(
            Resources.Load<SceneChangerUIView>("SceneChangerUIView"),
            _gameUIView.ChangeSceneContainer
        );

    void AddViewListeners ()
    {
        _view.OnClick += HandleViewClick;
    }
    
    //TODO pedro: views don't neet to remove listeners yet because there's no persistent ui
    void RemoveViewListeners ()
    {
        _view.OnClick -= HandleViewClick;
    }

    void HandleViewClick () => _gameUIView.FadeIn(ChangeScene);

    void ChangeScene ()
    {
        string sceneName = _gameSessionInfoProvider.CurrentSceneIndex == 1
            ? GetSceneNameFromBuildIndex(2)
            : GetSceneNameFromBuildIndex(1);

        _model.ChangeScene(sceneName);
    }

    string GetSceneNameFromBuildIndex (int index)
    {
        //TODO pedro: this is horrible
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }
}