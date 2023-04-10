using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticleAfterPlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void Update()
    {
        if (!particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
