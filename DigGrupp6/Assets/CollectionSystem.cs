using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{

    ParticleSystem ps;
    [SerializeField] float ammount;
    [SerializeField] AmmoTypeClass ammoToAdd;

    float counter;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
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

            if (ammount < 1)
            {
                counter += ammount;
                if (counter >= 1)
                {
                    ammoToAdd.AddAmmo(1);
                    counter = 0;
                }
            }

            if (ammount >= 1)
            {
                ammoToAdd.AddAmmo((int)ammount);
            }


        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
