using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyListenere : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Destroy", this);
        Debug.Break();
    }
}
