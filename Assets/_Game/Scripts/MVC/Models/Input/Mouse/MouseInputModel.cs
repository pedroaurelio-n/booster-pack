using UnityEngine;

public class MouseInputModel : IMouseInputModel
{
    public Vector3 CurrentPosition { get; private set; }
    public bool IsHoveringInteractable => _currentInteractable != null;

    Camera _mainCamera;
    IMouseInteractable _currentInteractable;
    IMouseClickable _currentClickable;

    public void SetMainCamera (Camera mainCamera) => _mainCamera = mainCamera;
    
    public void UpdatePosition (Vector3 position)
    {
        CurrentPosition = position;
        EvaluateRaycast();
    }

    public void LeftClick ()
    {
        if (_currentInteractable == null)
            return;
        
        _currentClickable?.OnLeftClick();
    }

    public void RightClick ()
    {
        if (_currentInteractable == null)
            return;
        
        _currentClickable?.OnLeftClick();
    }

    void EvaluateRaycast ()
    {
        Ray ray = _mainCamera.ScreenPointToRay(CurrentPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IMouseInteractable interactable = hit.collider.GetComponent<IMouseInteractable>();
            IMouseClickable clickable = hit.collider.GetComponent<IMouseClickable>();

            if (interactable == null)
                return;
            
            if (_currentInteractable == interactable)
                return;

            _currentInteractable?.OnExit();
            _currentInteractable = interactable;
            _currentClickable = clickable;
            _currentInteractable?.OnEnter();
        }
        else if (_currentInteractable != null)
        {
            _currentInteractable.OnExit();
            _currentInteractable = null;
            _currentClickable = null;
        }
    }
}