using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : BaseBall
{
	[SerializeField]
	float m_bounceForce = 10.0f;
	[SerializeField]
	float m_bounceTime = 10.0f;
	[SerializeField]
	float m_bounceSubScaler = 1.0f;

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);

		if (collision.gameObject.tag == "Ball")
		{
			List<ContactPoint2D> contacts = new List<ContactPoint2D>();
			collision.GetContacts(contacts);
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
				-contacts[0].normal * m_bounceForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
			collision.gameObject.GetComponent<BaseBall>().RegisterBounce(m_bounceForce * Time.fixedDeltaTime,
				m_bounceSubScaler, m_bounceTime);
		}
	}
}
