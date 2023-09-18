using UnityEngine;

public class GameUIView : MonoBehaviour
{
    [field: SerializeField] public Transform CardContainer { get; private set; }
    [field: SerializeField] public Transform ButtonContainer { get; private set; }
}