using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.InterfaceField;
using System;

namespace ML.GamePlay
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] SingleInterfaceField<IPositionChanged> carTransformWasChanged;
        [SerializeField] Transform transformToFollow;
        [SerializeField] bool followX = false;
        [SerializeField] bool followY = false;
        [SerializeField] bool followZ = false;

        Vector3 initialOffset;

        private void OnValidate()
        {
            carTransformWasChanged.Validate();
        }
        private void Awake()
        {
            initialOffset = transform.position - transformToFollow.position;
            carTransformWasChanged.Interface.OnPositionChanged += HandlePositionWasChanged;
        }

        private void OnDestroy()
        {
            carTransformWasChanged.Interface.OnPositionChanged -= HandlePositionWasChanged;
        }

        private void HandlePositionWasChanged()
        {
            Follow();
        }

        private void LateUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            Vector3 newPosition = transform.position;
            if(followZ)
                newPosition.z = transformToFollow.position.z + initialOffset.z;
            if(followY)
                newPosition.y = transformToFollow.position.y + initialOffset.y;
            if (followX)
                newPosition.x = transformToFollow.position.x + initialOffset.x;
            transform.position = newPosition;
        }
    }
}
