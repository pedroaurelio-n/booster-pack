using UnityEngine;

public class PoolableUIView : MonoBehaviour
{
    public bool IsActive => gameObject.activeInHierarchy;
}