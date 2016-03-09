using UnityEngine;
using System.Collections;
using System;

public class Killable : Targetable
{
	public GameObject[] DeadBodies;
	public float DeadBodyForce = 10f;

	public void Start ()
	{
        InitializeHealth(DesiredHealth);
        InitializeHealAmount(DesiredHealAmount);

        DeathEventHandler += (sender, args) =>
        {
            var deathArgs = (DeathEventArgs)args;
            Die(deathArgs.Direction, deathArgs.ImpactForce);
        };
	}

	public void Update ()
	{
		base.Step();
	}

	protected virtual void Die(Vector3 direction, float impactForce)
	{
		Debug.Log("dying");
		for (int i = 0; i < DeadBodies.Length; i++)
		{
			GameObject body = (GameObject)Instantiate(DeadBodies[i], transform.position, transform.localRotation);
			body.transform.localScale = transform.localScale;
			body.transform.rotation = transform.rotation;
			Rigidbody[] rbs = body.GetComponentsInChildren<Rigidbody>();
			for (int j = 0; j < rbs.Length; j++)
			{
				Vector3 randDir = new Vector3(UnityEngine.Random.Range(-DeadBodyForce, DeadBodyForce),
					UnityEngine.Random.Range(-DeadBodyForce, DeadBodyForce), 0);
				rbs[j].AddForce(direction * impactForce);
			}
		}

		Destroy(gameObject);
		Destroy(this);
	}
}
