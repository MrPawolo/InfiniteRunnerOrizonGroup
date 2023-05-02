using UnityEngine;

namespace ML.PhysicsEventsCaller
{
    public class OnCollisionEnterCaller : PhysicsEventsCallerBase
    {
        private void OnCollisionEnter(Collision collision)
        {
            HandlePhysicsEvent(collision, (call) => call.OnCollision(collision));
        }
    }
}
