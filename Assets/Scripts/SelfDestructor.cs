using UnityEngine;
using System.Collections;

public class SelfDestructor : MonoBehaviour
{
	public int Lifetime = 1;

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
