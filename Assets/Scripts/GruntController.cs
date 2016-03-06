using UnityEngine;
using System.Collections;
using System;

public class GruntController : LayeredController
{
    public GameObject[] DeadBodies;
    public float DeadBodyForce = 10f;

    private Targetable _targetable;

    // Use this for initialization
    public void Start ()
	{
		base.Initialize();
        _targetable = gameObject.GetComponent<Targetable>();
        _targetable.DeathEventHandler += (object sender, EventArgs args) =>
        {
            var deathArgs = (DeathEventArgs)args;
            Die(deathArgs.Direction, deathArgs.ImpactForce);
        };
    }
	
	// Update is called once per frame
	public void Update ()
	{
		base.Step();
	}

    public virtual void Die(Vector3 direction, float impactForce)
    {
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
