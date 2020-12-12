using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
	[SerializeField]
	Vector3 m_firstForce = Vector3.zero;
	[SerializeField]
	float m_firstTorque = 0.0f;
	[SerializeField]
	float m_hitTorqueRangeStart = 0.0f;
	[SerializeField]
	float m_hitTorqueRangeEnd = 0.0f;

	protected virtual bool m_isManualHitSe { get { return false; } }

	Rigidbody2D m_rigidbody = null;
	float m_maxSpeed = 0.0f;
	float m_addMaxSpeed = 0.0f;
	float m_nowAddMaxSpeed = 0.0f;
	float m_addMaxSpeedSubScaler = 0.0f;
	float m_addMaxTime = 0.0f;

	public void RegisterBounce(float addMaxSpeed,
		float addMaxSpeedSubScaler, float addMaxTime)
	{
		m_nowAddMaxSpeed = m_addMaxSpeed = addMaxSpeed;
		m_addMaxSpeedSubScaler = addMaxSpeedSubScaler;
		m_addMaxTime = addMaxTime;
	}

	// Start is called before the first frame update
	protected virtual void Start()
    {
		BallManager.instance.RegisterBall();

		m_rigidbody = GetComponent<Rigidbody2D>();
		m_rigidbody.AddForce(m_firstForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
		m_rigidbody.AddTorque(m_firstTorque * Time.fixedDeltaTime, ForceMode2D.Impulse);

		m_maxSpeed = m_rigidbody.velocity.magnitude;
	}
	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (!m_isManualHitSe)
			BallManager.instance.PlayHitSound(collision.gameObject.tag == "Ball");

		m_rigidbody.AddTorque(Random.Range(m_hitTorqueRangeStart, m_hitTorqueRangeEnd)
			* Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	protected virtual void FixedUpdate()
	{
		if (transform.position.y < BallManager.instance.deathBallHeight)
		{
			BallManager.instance.DeathBall(gameObject);
			return;
		}

		m_addMaxTime -= Time.fixedDeltaTime;
		if (m_addMaxTime <= 0.0f)
		{
			m_nowAddMaxSpeed -= m_addMaxSpeed * m_addMaxSpeedSubScaler * Time.fixedDeltaTime;
			if (m_nowAddMaxSpeed <= 0.0f) m_nowAddMaxSpeed = 0.0f;
		}

		m_rigidbody.velocity = m_rigidbody.velocity.normalized * (m_maxSpeed + m_nowAddMaxSpeed);
	}
}
