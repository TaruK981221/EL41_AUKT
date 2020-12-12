using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBall : BaseBall
{
	[SerializeField]
	GameObject m_explosionEffect = null;
	[SerializeField]
	float m_explosionTimeRangeStart = 5.0f;
	[SerializeField]
	float m_explosionTimeRangeEnd = 10.0f;

	float m_counter = 0.0f;

	protected override void Start()
	{
		base.Start();
		m_counter = Random.Range(m_explosionTimeRangeStart, m_explosionTimeRangeEnd);
	}

	// Update is called once per frame
	void Update()
    {
		m_counter -= Time.deltaTime;

		if (m_counter <= 0.0f)
		{
			var obj = Instantiate(m_explosionEffect);
			obj.transform.position = transform.position;
			obj.SetActive(true);
			Destroy(gameObject);
		}
	}
}
