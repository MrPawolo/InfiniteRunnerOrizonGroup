using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.RootShift;
public class SetParentToRoot : MonoBehaviour
{
    [SerializeField] bool autoParentOnAwake = true;
    private void Awake()
    {
        if (autoParentOnAwake)
            SetParent();
    }
    public void SetParent()
    {
        transform.SetParent(RootShiftManager.Root);
    }
}
