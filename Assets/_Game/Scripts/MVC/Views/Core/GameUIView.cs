using UnityEngine;

public class GameUIView : MonoBehaviour
{
    
    [field: SerializeField] public FadeToBlackManager FadeToBlackManager { get; private set; }
    [field: SerializeField] public Transform ButtonContainer { get; private set; }
    [field: SerializeField] public Transform ChangeSceneContainer { get; private set; }

}