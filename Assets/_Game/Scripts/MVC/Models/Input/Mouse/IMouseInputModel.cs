using UnityEngine;

public interface IMouseInputModel
{
    Vector3 CurrentPosition { get; }
    bool IsHoveringInteractable { get; }

    void SetMainCamera (Camera mainCamera);
    void UpdatePosition (Vector3 position);
    void LeftClick ();
    void RightClick ();
}