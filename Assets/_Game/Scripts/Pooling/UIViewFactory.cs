using System.Collections.Generic;
using UnityEngine;

public class UIViewFactory
{
    readonly Dictionary<string, ObjectPool<PoolableUIView>> _pools = new();

    public void SetupPool<T> (string poolName, T prefab, Transform container) where T : PoolableUIView
    {
        _pools.TryAdd(poolName, new ObjectPool<PoolableUIView>(prefab, container));
    }

    public T GetView<T> (string poolName) where T : PoolableUIView
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableUIView> pool))
            return default;

        return pool.Get() as T;
    }
    
    public void ReleaseView (string poolName, PoolableUIView view)
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableUIView> pool))
            return;

        pool.Release(view);
    }
}