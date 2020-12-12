using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBall : BaseBall
{
	[SerializeField]
	float m_minSize = 0.5f;
	[SerializeField]
	float m_maxSize = 1.0f;
	[SerializeField]
	float m_scalingSpeed = 1.0f;

	float m_nowSize = 0.0f;
	float m_counter = 0.0f;

	protected override void Start()
	{
		base.Start();

		m_nowSize = transform.localScale.x;
		m_counter = Time.time;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		List<Collider2D> results = new List<Collider2D>();
		float nextCounter = m_counter + Time.fixedDeltaTime * m_scalingSpeed;
		float nextSize = m_minSize + Mathf.PingPong(nextCounter, m_maxSize - m_minSize);

		Physics2D.OverlapCircle(transform.position, nextSize, new ContactFilter2D(), results);
		for (int i = 0; i < results.Count;)
		{
			if (results[i].gameObject == gameObject)
				results.RemoveAt(i);
			else ++i;
		}

		if (results.Count == 0)
		{
			m_nowSize = nextSize;
			m_counter = nextCounter;
			transform.localScale = new Vector3(nextSize, nextSize, 1.0f);
		}
	}
}
