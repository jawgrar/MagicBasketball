// 2023-12-16 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem ps; // Particle system reference[^2^]

    void Start()
    {
        ps = GetComponent<ParticleSystem>(); // Assign the particle system component[^3^]
    }

    void Update()
    {
        if (ps && !ps.IsAlive()) // If the particle system is not null and not alive[^4^]
        {
            Destroy(gameObject); // Destroy the game object[^5^]
        }
    }
}
