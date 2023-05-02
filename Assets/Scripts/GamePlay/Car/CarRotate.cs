using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class CarRotate : MonoBehaviour
    {
        [SerializeField] AnimationCurve velocityToRotation;
        [SerializeField] AnimationCurve velocityToRoll;

        float previousXPos;

        private void Awake()
        {
            previousXPos = transform.position.x;
        }

        public void RotateCar()
        {
            float currentVelocity = transform.position.x - previousXPos;
            previousXPos = transform.position.x;

            Quaternion rot = Quaternion.Euler(
                0,
                velocityToRotation.Evaluate(Mathf.Abs(currentVelocity)) * Mathf.Sign(currentVelocity),
                0);

            Quaternion roll = Quaternion.Euler(
                0,
                0,
                velocityToRoll.Evaluate(Mathf.Abs(currentVelocity)) * Mathf.Sign(currentVelocity));

            transform.rotation = rot * roll;
        }
    }
}