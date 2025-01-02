using UnityEngine;

public interface IPhysicsProvider
{
    bool Raycast (Ray ray, out RaycastHit hit);
    bool Raycast (Ray ray, int layerMask, out RaycastHit hit);
}