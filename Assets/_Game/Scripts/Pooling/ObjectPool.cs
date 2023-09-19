using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class ObjectPool<T> where T : PoolableUIView
{
    readonly HashSet<T> _activeObjects = new();
    readonly HashSet<T> _inactiveObjects = new();
    readonly T _prefab;
    readonly Transform _container;

    public ObjectPool (T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;
    }

    public T Get ()
    {
        if (_inactiveObjects.Count == 0)
            return Create();

        T obj = Enumerable.First(_inactiveObjects);
        _inactiveObjects.Remove(obj);
        _activeObjects.Add(obj);
        obj.transform.SetParent(_container);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release (T obj)
    {
        if (!_activeObjects.Contains(obj))
            return;

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(null);
        _activeObjects.Remove(obj);
        _inactiveObjects.Add(obj);
    }

    T Create ()
    {
        T obj = GameObject.Instantiate(_prefab, _container);
        _activeObjects.Add(obj);
        obj.gameObject.SetActive(true);
        return obj;
    }
}