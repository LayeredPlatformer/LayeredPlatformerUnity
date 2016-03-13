using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour 
{
	public float FlapPeriod = .1f;
	public float FlapPower = 100f;
	public float Range = 15f;

	private Transform Player;
	private Rigidbody rb;
	private int targetOffset = 2;
	private int randomFlapForceMin = 1;
	private int randomFlapForceMax = 40;
	private Vector3 homePosition;
	private bool hasInitializedHomePosition = false;

	void Start ()
	{
		Invoke("Flap", FlapPeriod);
		rb = gameObject.GetComponent<Rigidbody>();
		Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Flap()
	{
		Invoke("Flap", FlapPeriod);
		Vector3 tpos = new Vector3(Player.position.x, Player.position.y+targetOffset, Player.position.z);

		Vector3 tdir = tpos - transform.position;
		bool sameLayers = GetComponent<LayeredController>().Layer.Equals(Player.GetComponent<LayeredController>().Layer);
		if (tdir.magnitude > Range || !sameLayers)
		{
			if (!hasInitializedHomePosition)
			{
				homePosition = transform.position;
				hasInitializedHomePosition = true;
			}
			tdir = homePosition - transform.position;
		}
		else
			hasInitializedHomePosition = false;

		if (transform.position.y  < tpos.y)
		{
			tdir = tdir.normalized*FlapPower;
			Vector3 rand = new Vector3(Random.Range(randomFlapForceMin, randomFlapForceMax),
				Random.Range(randomFlapForceMin, randomFlapForceMax), 0);
			tdir += rand;
			rb.AddForce(tdir);
		}
	}
}
