using NSubstitute;
using NUnit.Framework;

namespace GameTests.SceneChanger
{
    public class SceneChangerModelTests
    {
        class BaseSceneChangerModelTests
        {
            protected ILoadingManager LoadingManager { get; private set; }
            
            protected SceneChangerModel Model { get; private set; }

            [SetUp]
            public void Setup ()
            {
                LoadingManager = Substitute.For<ILoadingManager>();
                
                Model = new SceneChangerModel(LoadingManager);
            }
        }

        class ChangeScene : BaseSceneChangerModelTests
        {
            [Test]
            public void Assert_ChangeScene_Received ()
            {
                const string NEW_SCENE = "Scene1";
                Model.ChangeScene(NEW_SCENE);
                LoadingManager.Received().LoadNewScene(NEW_SCENE);
            }
        }
    }
}