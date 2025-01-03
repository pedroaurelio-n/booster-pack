using UnityEngine;

public class MouseInputModel : IMouseInputModel
{
    public Vector3 CurrentPosition { get; private set; }
    public bool IsHoveringInteractable => _currentInteractable != null;

    readonly IPhysicsProvider _physicsProvider;

    Camera _mainCamera;
    IMouseInteractable _currentInteractable;
    IMouseClickable _currentClickable;
    Collider _currentCollider;
    LayerMask _interactableLayer;

    public MouseInputModel (IPhysicsProvider physicsProvider)
    {
        _physicsProvider = physicsProvider;
        //TODO pedro: Find a better way to get the layer, maybe with game settings
        _interactableLayer = LayerMask.GetMask($"Interactable");
    }

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
        
        _currentClickable?.OnRightClick();
    }

    void EvaluateRaycast ()
    {
        Ray ray = _mainCamera.ScreenPointToRay(CurrentPosition);

        if (_physicsProvider.Raycast(ray, _interactableLayer, out RaycastHit hit))
        {
            if (hit.collider == _currentCollider)
                return;

            _currentCollider = hit.collider;
            
            IMouseInteractable interactable = _currentCollider.GetComponent<IMouseInteractable>();
            IMouseClickable clickable = _currentCollider.GetComponent<IMouseClickable>();

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
            _currentCollider = null;
        }
    }
}