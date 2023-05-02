using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.RootShift;

public class DisableDebug : MonoBehaviour
{
    Vector3 realPrevPos;

    private void Start()
    {
        realPrevPos = transform.position.ShiftedToReal();
    }
    private void Update()
    {
        Vector3 newPos = transform.position.ShiftedToReal();
        //if(Vector3.Distance(newPos,realPrevPos) > 20)
        //{
        //    Debug.Log("ss", this);
        //    Debug.Break();
        //}
        realPrevPos = newPos;
    }

    private void OnDisable()
    {
        if(Camera.main.transform.position.ShiftedToReal().z < realPrevPos.z)
        {
            Debug.Log("sssss", this);
            Debug.Break();
        }

        //if (Camera.main.transform.position.z < transform.position.z)
        //{
        //    Debug.Log("sad", this);
        //    Debug.Break();
        //}
    }
}
