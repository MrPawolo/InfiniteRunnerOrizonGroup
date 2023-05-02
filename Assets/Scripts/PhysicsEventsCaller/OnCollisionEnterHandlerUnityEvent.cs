using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.PhysicsEventsCaller
{
    public class OnCollisionEnterHandlerUnityEvent : OnCollisionEnterHandler
    {
        [SerializeField] 
        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
        }
    }
}
