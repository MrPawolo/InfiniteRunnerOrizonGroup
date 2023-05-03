using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ML.Extensions;
using ML.PhysicsEventsCaller;

namespace ML.GamePlay
{
    public class CollisionCaller : OnCollisionEnterHandler
    {
        [SerializeField] UnityEvent<Collision> onCollision;
        [SerializeField] LayerMask obstacleMask;

        public override void OnCollision(Collision collision)
        {
            if (!obstacleMask.ContainsLayer(collision.gameObject.layer))
                return;

            onCollision.Invoke(collision);
        }

       
    }
}