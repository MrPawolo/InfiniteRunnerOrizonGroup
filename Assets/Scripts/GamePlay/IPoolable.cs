using System;
using UnityEngine;

namespace ML.GamePlay
{
    public interface IPoolable
    {
        public Action<GameObject> onRelease { get;set; }
        public Action onGet { get; set; }

        public void ForceRelease();
    }
}
