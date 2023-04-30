using System.Runtime.CompilerServices;
using UnityEngine;

namespace ML.RootShift
{
    public static class ExtensionVectorRootShift
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RealToShifted(this Vector3 position)
        {
            return RootShiftManager.RealToShifted(position);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ShiftedToReal(this Vector3 position)
        {
            return RootShiftManager.ShiftedToReal(position);
        }
    }
}