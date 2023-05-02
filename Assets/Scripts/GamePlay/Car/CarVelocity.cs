using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class CarVelocity : MonoBehaviour
    {
        [SerializeField] float accel;
        [SerializeField] Rigidbody rb;
        [SerializeField] float holdSpring = 2;
        [SerializeField] float holdDamper = 0.1f;
        public float Vel { get; set; }

        private float currentCalVel;
        private float targetHeight;
        private float actVel;

        private void Start()
        {
            targetHeight = transform.position.y;
        }

        public void ResetValues()
        {
            currentCalVel = 0;
            actVel = 0; 
        }

        public void VelocitiesOnCar()
        {
            Vector3 rbPosition = Vector3.zero;
            rbPosition = CalculateVerticalHoldingVelocity(rbPosition);
            rbPosition = CalculateForwardVelocity(rbPosition);
            rb.velocity = rbPosition;
        }

        private Vector3 CalculateForwardVelocity(Vector3 rbPosition)
        {
            actVel = Mathf.SmoothDamp(actVel, Vel, ref currentCalVel, 0.1f,accel);
            rbPosition.z = actVel;
            return rbPosition;
        }

        private Vector3 CalculateVerticalHoldingVelocity(Vector3 rbPosition)
        {
            float distance = targetHeight - rb.position.y;
            float velY = rb.velocity.y;
            float newVelY = distance * holdSpring - velY * holdDamper;
            rbPosition.y = newVelY;
            return rbPosition;
        }
    }
}