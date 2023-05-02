using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ML.Extensions
{
    public static class LayerMaskExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsLayer(this LayerMask layerMask,int layer)
        {
            return ((1 << layer) & layerMask) != 0;
        }
    }
}
