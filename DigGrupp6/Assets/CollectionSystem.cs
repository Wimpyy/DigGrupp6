using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{

    ParticleSystem ps;
    [SerializeField] int ammount;
    [SerializeField] AmmoTypeClass ammoToAdd;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }


    private void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            particles[i] = p;

            ammoToAdd.AddAmmo(ammount);
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
