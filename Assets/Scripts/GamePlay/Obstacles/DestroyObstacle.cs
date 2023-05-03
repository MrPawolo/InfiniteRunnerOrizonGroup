using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ML.InterfaceField;

namespace ML.GamePlay
{
    public class DestroyObstacle : MonoBehaviour, IDestroy
    {
        [SerializeField] SingleInterfaceField<IPoolable> poolable;
        [SerializeField] UnityEvent onDestroy;

        private void OnValidate()
        {
            poolable.Validate();
        }
        public void Destroy()
        {
            onDestroy.Invoke();
            ((IPoolable)poolable.Interface).ForceRelease();
        }
    }
}
