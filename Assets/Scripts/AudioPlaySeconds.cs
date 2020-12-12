using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaySeconds : MonoBehaviour
{
	[SerializeField]
	AudioSource m_audioSource = null;
	[SerializeField]
	float m_playSeconds = 0.0f;

	float m_counter = 0.0f;
	
    // Update is called once per frame
    void Update()
    {
		if (m_counter >= 0.0f)
		{
			m_counter += Time.deltaTime;
			if (m_playSeconds <= m_counter)
			{
				m_audioSource.Play();
				m_counter = -1;
			}
		}
    }
}
