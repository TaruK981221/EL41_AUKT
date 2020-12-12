using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoroyWhenParticleEnd : MonoBehaviour
{
	ParticleSystem[] m_particles = null;
    // Start is called before the first frame update
    void Start()
    {
		m_particles = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		foreach (var particle in m_particles)
			if (particle.isPlaying) return;

		Destroy(gameObject);
    }
}
