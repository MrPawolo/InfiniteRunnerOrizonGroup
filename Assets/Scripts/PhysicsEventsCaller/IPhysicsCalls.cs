using UnityEngine;

namespace ML.PhysicsEventsCaller
{
    public interface IPhysicsCalls
    {
        void OnCollision(Collision collision);
        void OnTrigger(Collider collider);
    }
}
