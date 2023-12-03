using UnityEngine;

public interface IPhysicsProvider
{
    bool Raycast (Ray ray, out RaycastHit hit);
}