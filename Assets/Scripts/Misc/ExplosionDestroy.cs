using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioSource source;

    private void Update()
    {
        if (!source.isPlaying && !particle.isPlaying)
            Destroy(gameObject);
    }
}
