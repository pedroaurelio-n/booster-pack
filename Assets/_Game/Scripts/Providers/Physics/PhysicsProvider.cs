using UnityEngine;

public class PhysicsProvider : IPhysicsProvider
{
    public bool Raycast (Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit);
    }
    
    public bool Raycast (Ray ray, int layerMask, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit, float.PositiveInfinity, layerMask);
    }
}