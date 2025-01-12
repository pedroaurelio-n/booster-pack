using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterPackPurchaseUIView : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI nameText;

    void Awake ()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void SetPackName (string text) => nameText.text = text;
}