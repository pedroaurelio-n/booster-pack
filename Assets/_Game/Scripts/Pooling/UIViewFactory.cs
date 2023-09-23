using System.Collections.Generic;
using UnityEngine;

public class UIViewFactory
{
    readonly Dictionary<string, ObjectPool<PoolableView>> _pools = new();

    public void SetupPool<T> (string poolName, T prefab, Transform container) where T : PoolableView
    {
        _pools.TryAdd(poolName, new ObjectPool<PoolableView>(prefab, container));
    }

    public T GetView<T> (string poolName) where T : PoolableView
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
            return default;

        return pool.Get() as T;
    }
    
    public void ReleaseView (string poolName, PoolableView view)
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
            return;

        pool.Release(view);
    }
}