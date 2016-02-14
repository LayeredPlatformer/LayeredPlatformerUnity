using UnityEngine;
using System.Collections;

public class SelfDestructor : MonoBehaviour
{
	public float Lifetime = 1f;

	void Start()
	{
		Initialize();
	}

	protected void Initialize()
	{
		Invoke("Die", Lifetime);
	}

	void Die()
	{
		Destroy(gameObject);
		Destroy(this);
	}
}
