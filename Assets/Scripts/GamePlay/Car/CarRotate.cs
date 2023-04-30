using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class CarRotate : MonoBehaviour
    {
        [SerializeField] AnimationCurve velocityToRotation;
        [SerializeField] AnimationCurve velocityToRoll;


        public void RotateCar(float currentVelocity)
        {
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