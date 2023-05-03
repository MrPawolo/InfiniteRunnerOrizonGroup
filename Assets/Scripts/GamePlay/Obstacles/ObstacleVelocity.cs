using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.InterfaceField;
using ML.RootShift;

namespace ML.GamePlay
{
    public class ObstacleVelocity : MonoBehaviour
    {
        [SerializeField] SingleInterfaceField<IPoolable> pollableObstacle;
        [SerializeField] Vector2 minMaxVelocity = new Vector2(10, 15);
        [SerializeField] LayerMask obstacleMask;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform rayCheckStart;

        float MAX_RAY_DISTANCE = 2;
        float currentVelocity;
        bool hitPreviously = false;
        float prevHitPosition;
        private void OnValidate()
        {
            pollableObstacle.Validate();
        }

        private void Awake()
        {
            ((IPoolable)pollableObstacle.Interface).onGet += HandleOnGet;
            ((IPoolable)pollableObstacle.Interface).onRelease += HandleOnRelease;
        }

        private void OnDestroy()
        {
            ((IPoolable)pollableObstacle.Interface).onGet -= HandleOnGet;
            ((IPoolable)pollableObstacle.Interface).onRelease -= HandleOnRelease;
        }

        private void HandleOnRelease(GameObject obj)
        {
            currentVelocity = 0;
        }

        private void HandleOnGet()
        {
            currentVelocity = Random.Range(minMaxVelocity.x, minMaxVelocity.y);
        }

        private void Update()
        {
            AdjustVelocityToInfrontObstacle();

            //rb.MovePosition(transform.position +
            //    Vector3.forward * currentVelocity * Time.deltaTime);
            transform.position += Vector3.forward * currentVelocity * Time.deltaTime;

        }

        private void AdjustVelocityToInfrontObstacle()
        {
            Ray ray = new Ray(rayCheckStart.transform.position, rayCheckStart.forward);
            bool hit = Physics.Raycast(ray, out RaycastHit firstHit, MAX_RAY_DISTANCE,obstacleMask);
            float hitPos = firstHit.point.ShiftedToReal().z;

            if (hitPreviously && hit)
            {
                float inFrontObstacleVelocity = (hitPos - prevHitPosition) / Time.deltaTime;
                currentVelocity = inFrontObstacleVelocity - 0.1f;
            }
            prevHitPosition = hitPos;
            hitPreviously = hit;
        }
    }
}
