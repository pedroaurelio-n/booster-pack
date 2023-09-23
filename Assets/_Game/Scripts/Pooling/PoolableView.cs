using UnityEngine;

public class PoolableView : MonoBehaviour
{
    public bool IsActive => gameObject.activeInHierarchy;
}