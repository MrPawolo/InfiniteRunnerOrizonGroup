using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendShaderPos : MonoBehaviour
{
    public string id;
    int POS_ID;

    private void Awake()
    {
        POS_ID = Shader.PropertyToID(id);
    }

    void LateUpdate()
    {
        Shader.SetGlobalVector(POS_ID, transform.position);
    }
}
