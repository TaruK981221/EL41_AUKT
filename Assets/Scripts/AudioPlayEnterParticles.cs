using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayEnterParticles : MonoBehaviour
{
	[SerializeField]
	ParticleSystem m_particle = null;
	[SerializeField]
	AudioSource m_audioSource = null;

	bool m_isPlay = false;

    // Update is called once per frame
    void Update()
    {
		if (!m_isPlay && m_particle.isPlaying)
		{
			m_audioSource.Play();
		}
    }
}
