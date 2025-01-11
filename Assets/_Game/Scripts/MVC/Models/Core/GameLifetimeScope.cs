using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public static GameLifetimeScope Instance { get; private set; }

    protected override void Configure (IContainerBuilder builder)
    {
        base.Configure(builder);
        Instance = this;
        DontDestroyOnLoad(this);
    }
}