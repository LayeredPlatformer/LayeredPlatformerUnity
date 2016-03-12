using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour 
{
	public float FlapPeriod = .1f;
	public float FlapPower = 100f;
	public float Range = 20f;

	private Transform Player;
	private Rigidbody rb;
	private int targetOffset = 2;
	private int randomFlapForceMin = 1;
	private int randomFlapForceMax = 40;

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
		if (tdir.magnitude > Range)
		{
			rb.useGravity = false;
			return;
		}
		rb.useGravity = true;

		if (transform.position.y  < tpos.y)
		{
			tdir = tdir.normalized*FlapPower;
			Vector3 rand = new Vector3(Random.Range(randomFlapForceMin, randomFlapForceMax),
				Random.Range(randomFlapForceMin, randomFlapForceMax),
				Random.Range(randomFlapForceMin, randomFlapForceMax));
			tdir += rand;
			rb.AddForce(tdir);
		}
	}
}
