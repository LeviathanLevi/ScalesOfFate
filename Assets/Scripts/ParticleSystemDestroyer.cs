using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroyer : MonoBehaviour
{
    IEnumerator DestroyAfterPlaying(ParticleSystem ps)
    {
        while (ps.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(ps.gameObject);
    }

    void Start()
    {
        // Assume 'myParticleSystem' is the Particle System you want to destroy.
        StartCoroutine(DestroyAfterPlaying(GetComponent<ParticleSystem>()));
    }
}
