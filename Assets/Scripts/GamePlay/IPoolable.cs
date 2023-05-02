using System;
using UnityEngine;

namespace ML.GamePlay
{
    public interface IPoolable
    {
        public Action<GameObject> forceRelease { get;set; }
        public Action onGet { get; set; }
    }
}
