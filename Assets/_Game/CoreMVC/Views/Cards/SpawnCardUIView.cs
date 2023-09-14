using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCardUIView : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] Button button;

    void Awake ()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}