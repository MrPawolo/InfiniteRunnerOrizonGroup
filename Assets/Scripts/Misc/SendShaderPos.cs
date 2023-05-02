using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SendShaderPos : MonoBehaviour
{
    public string id;
    public float BendPower = 3;
    public float BendMultiplier = 0.1f;
    public float SideBendPower = 0;
    [Range(-1e-8f, 1e-8f)] public float SideBendMultiplier = 0;
    int POS_ID;
    int BEND_POWER_ID;
    int BEND_MULTIPLIER_ID;
    int SIDE_BEND_POWER_ID;
    int SIDE_BEND_MULTIPLIER_ID;

    private void Awake()
    {
        POS_ID = Shader.PropertyToID(id);
        BEND_POWER_ID = Shader.PropertyToID("_BendPower");
        BEND_MULTIPLIER_ID = Shader.PropertyToID("_BendMultiplier");
        SIDE_BEND_POWER_ID = Shader.PropertyToID("_SideBendPower");
        SIDE_BEND_MULTIPLIER_ID = Shader.PropertyToID("_SideBendMultiplier");
    }

    void LateUpdate()
    {
        Shader.SetGlobalVector(POS_ID, transform.position);
        Shader.SetGlobalFloat(BEND_POWER_ID, BendPower);
        Shader.SetGlobalFloat(BEND_MULTIPLIER_ID, BendMultiplier);
        Shader.SetGlobalFloat(SIDE_BEND_POWER_ID, SideBendPower);
        Shader.SetGlobalFloat(SIDE_BEND_MULTIPLIER_ID, SideBendMultiplier);
    }
}
