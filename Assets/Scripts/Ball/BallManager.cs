using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class BallManager : MonoBehaviour
{
	public static BallManager instance { get; private set; } = null;
	public float deathBallHeight { get { return m_deathBallHeight; } }
	public int numBalls { get; private set; } = 0;

	[SerializeField]
	GameObject m_deathEffect = null;
	[SerializeField]
	AudioSource m_deathSound = null;
	[SerializeField]
	AudioSource m_hitSound = null;
	[SerializeField]
	AudioSource m_hitSoundToBall = null;
	[SerializeField]
	float m_deathBallHeight = -10.0f;

	void Awake()
	{
		instance = this;
	}

	public void RegisterBall()
	{
		++numBalls;
	}
	public void PlayHitSound(bool isBall)
	{
		if (!isBall) m_hitSound.PlayOneShot(m_hitSound.clip);
		else m_hitSoundToBall.PlayOneShot(m_hitSoundToBall.clip);
	}

	public void DeathBall(GameObject deathObject)
	{
		var obj = Instantiate(m_deathEffect);
		obj.transform.position = deathObject.transform.position;
		obj.SetActive(true);
		--numBalls;

		m_deathSound.PlayOneShot(m_deathSound.clip);
		Destroy(deathObject);
	}
}
