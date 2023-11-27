using System;
using UnityEngine;

public class MouseInputView : MonoBehaviour
{
    public event Action<Vector3> OnPositionChanged;
    public event Action OnLeftClick;
    public event Action OnRightClick;

    Vector3 _currentMousePosition;
    
    void Update ()
    {
        if (_currentMousePosition != Input.mousePosition)
        {
            _currentMousePosition = Input.mousePosition;
            OnPositionChanged?.Invoke(_currentMousePosition);
        }
        
        if (Input.GetMouseButtonDown(0))
            OnLeftClick?.Invoke();
        
        if (Input.GetMouseButtonDown(1))
            OnRightClick?.Invoke();
    }
}