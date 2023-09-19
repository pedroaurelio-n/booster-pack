using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIView : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI buttonText;

    void Awake ()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void SetText (string text) => buttonText.text = text;
}