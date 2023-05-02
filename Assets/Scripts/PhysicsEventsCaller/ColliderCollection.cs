using UnityEngine;

namespace ML.PhysicsEventsCaller
{
    [System.Serializable]
    public class ColliderCollection
    {
        public Collider[] colliders;
        public IPhysicsCalls physicsCall;

        public ColliderCollection(Collider[] colliders, IPhysicsCalls physicsCall)
        {
            this.colliders = colliders;
            this.physicsCall = physicsCall;
        }
    }

}
