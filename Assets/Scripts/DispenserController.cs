using UnityEngine;
using System.Collections;

public class DispenserController : Triggerable
{
	public float TriggerDelay = .5f;
	public GameObject DispenseItem;
	public float DispenseDelay = .5f;

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
			Instantiate(DispenseItem, _dispenseLocation, Quaternion.identity);
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
