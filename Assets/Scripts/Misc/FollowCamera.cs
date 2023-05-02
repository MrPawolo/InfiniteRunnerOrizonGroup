using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform cam;
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.position = cam.position;
    }
}
