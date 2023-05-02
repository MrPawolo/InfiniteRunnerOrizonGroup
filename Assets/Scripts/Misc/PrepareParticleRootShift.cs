using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.RootShift;

public class PrepareParticleRootShift : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] bool autoParentOnAwake = true;
    private void Awake()
    {
        if(autoParentOnAwake)
            SetSimulationSpaceToRoot();
    }

    public void SetSimulationSpaceToRoot()
    {
        ParticleSystem.MainModule main = particle.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Custom;
        main.customSimulationSpace = RootShiftManager.Root;
    }
}
