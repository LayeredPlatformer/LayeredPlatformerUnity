using UnityEngine;
using System.Collections;

public class DispenserController : Triggerable
{
	public float TriggerDelay = .5f;
	public SelfDestructor DispenseItem;
	public float DispenseDelay = .5f;
	public float DispenseSpeed = 5f;
	public float DispenseItemLifetime = 3f;

	private bool _dispensing = true;
	private Vector3 _dispenseLocation;

	void Start ()
	{
		_dispenseLocation = transform.Find("DispenseLocation").transform.position;
		Invoke("Dispense", DispenseDelay);
	}
	
	void Dispense()
	{
		if (_dispensing)
		{
            _dispenseLocation = transform.Find("DispenseLocation").transform.position;
			SelfDestructor sd = (SelfDestructor) Instantiate(DispenseItem, _dispenseLocation, Quaternion.identity);
			GameObject g = sd.gameObject;
			Rigidbody rb = g.GetComponent<Rigidbody>();
			rb.AddForce((_dispenseLocation-transform.position)*DispenseSpeed);
			sd.Lifetime = DispenseItemLifetime;
		}
		Invoke("Dispense", DispenseDelay);
	}

	void ResumeDispensing()
	{
		_dispensing = true;
	}

	public override void Trigger ()
	{
		_dispensing = false;
		CancelInvoke("ResumeDispensing");
		Invoke("ResumeDispensing", TriggerDelay);
	}
}
