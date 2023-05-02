using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.RootShift;
public class SetParentToRoot : MonoBehaviour
{
    public void SetParent()
    {
        transform.SetParent(RootShiftManager.Root);
    }
}
