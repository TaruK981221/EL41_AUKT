using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBall : BaseBall
{
	[SerializeField]
	GameObject m_explosionEffect = null;
	[SerializeField]
	AudioSource m_audioSource = null;
	[SerializeField]
	SpriteRenderer m_sprite = null;
	[SerializeField]
	float m_explosionTimeRangeStart = 5.0f;
	[SerializeField]
	float m_explosionTimeRangeEnd = 10.0f;

	float m_counter = 0.0f;
	float m_seCounter = 0.0f;
	bool m_isFirstPlay = false;

	protected override void Start()
	{
		base.Start();
		m_counter = Random.Range(m_explosionTimeRangeStart, m_explosionTimeRangeEnd);
	}

	// Update is called once per frame
	void Update()
    {
		m_counter -= Time.deltaTime;

		if (m_counter <= 1.0f)
		{
			m_seCounter += Time.deltaTime;
			if (m_seCounter < 0.1f)
				m_sprite.color = Color.red;
			else
				m_sprite.color = Color.white;

			if (m_seCounter > 0.2f)
			{
				m_seCounter -= 0.2f;
				m_audioSource.PlayOneShot(m_audioSource.clip);
			}
			else if (!m_isFirstPlay)
			{
				m_isFirstPlay = true;
				m_audioSource.PlayOneShot(m_audioSource.clip);
			}

			if (m_counter <= 0.0f)
			{
				var obj = Instantiate(m_explosionEffect);
				obj.transform.position = transform.position;
				obj.SetActive(true);
				Destroy(gameObject);
			}
		}
	}
}
