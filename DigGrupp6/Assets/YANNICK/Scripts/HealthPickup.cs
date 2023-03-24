using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    ParticleSystem particleSyst;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    public int healthPerParticle = 1;

    [SerializeField] float particleCounter = 0;
    float particleCountPP = 0.01f;

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
            if (FindObjectOfType<PlayerLifeSupport>().ifHearts)
            {
                Debug.Log("particle Check start");
                particleCounter += particleCountPP;
                if (particleCounter >= 1)
                {
                    FindObjectOfType<PlayerLifeSupport>().GainHealth(healthPerParticle);
                    particleCounter = 0;
                    Debug.Log("healthHearts Gained");
                }
            }
            else 
            {
                FindObjectOfType<PlayerLifeSupport>().GainHealth(healthPerParticle);
                Debug.Log("Healthbar Gained");
            }
            particles[i] = p;
        }
        particleSyst.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
