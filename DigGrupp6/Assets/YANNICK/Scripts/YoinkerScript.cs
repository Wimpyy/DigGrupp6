using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoinkerScript : MonoBehaviour
{


    ParticleSystem particleSyst;

    List<ParticleSystem.Particle> particles;

    private void Start()
    {
        particleSyst = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        int triggerParticles = particleSyst.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggerParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            Debug.Log("Yuuur");
            particles[i] = p;
        }
        particleSyst.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
