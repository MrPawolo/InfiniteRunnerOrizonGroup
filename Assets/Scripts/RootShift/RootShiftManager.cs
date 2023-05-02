using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ML.RootShift
{
    public class RootShiftManager : MonoBehaviour
    {
        [SerializeField, Min(0)] float moveDistance = 100f;
        [SerializeField] Transform rootTransform;
        [SerializeField] Transform rootReference;

        public static RootShiftManager Instance { get; private set; }
        public static Vector3 ShiftAmount { get; private set; }
        public static Transform Root { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
                DontDestroyOnLoad(this);
                Root = rootTransform;
            }
            else
            {
                Destroy(this);
            }

            if(rootReference == null)
            {
                rootReference = Camera.main.transform;
            }
        }

        private void OnDestroy()
        {
            if(Instance == this)
            {
                ShiftAmount = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            if (ShouldShift())
            {

                Vector3 projectedRootDifference = new Vector3(
                    rootReference.position.x,
                    0,
                    rootReference.position.z);
                ShiftAmount -= projectedRootDifference;
                rootTransform.position = ShiftAmount;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool ShouldShift()
        {
            Vector2 projectedReference = new Vector2(rootReference.position.x, rootReference.position.z);
            float distanceToRoot = projectedReference.magnitude;
            return distanceToRoot > moveDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RealToShifted(Vector3 position)
        {
            return position + ShiftAmount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ShiftedToReal(Vector3 position)
        {
            return position - ShiftAmount;
        }
    }
}