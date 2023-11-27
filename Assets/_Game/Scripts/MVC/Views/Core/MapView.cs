using UnityEngine;

public class MapView : MonoBehaviour
{
    [field: SerializeField] public Transform CardContainer { get; private set; }
    
    public MouseInputView MouseInput { get; private set; }

    public void Initialize ()
    {
        CreateInputs();
    }

    void CreateInputs ()
    {
        GameObject mouseInput = new("MouseInput");
        mouseInput.transform.SetParent(transform);
        MouseInput = mouseInput.AddComponent<MouseInputView>();
    }
}