using UnityEngine;
using System.Collections;

public class DispenserController : Triggerable
{
	public float TriggerDelay = .5f;
	public SelfDestructor DispenseItem;
	public float DispenseDelay = .5f;
	public float DispenseSpeed = 5f;
	public float DispenseItemLifetime = 3f;
	public bool Dispensing = true;
    private LayeredController _dispenserLayeredController;

    private Vector3 _dispenseLocation;

	void Start ()
	{
        _dispenserLayeredController = gameObject.GetComponent<LayeredController>();
        _dispenseLocation = transform.Find("DispenseLocation").transform.position;
		Invoke("Dispense", DispenseDelay);
	}

	void Dispense()
	{
		if (Dispensing)
		{
            _dispenseLocation = transform.Find("DispenseLocation").transform.position;
			SelfDestructor sd = (SelfDestructor) Instantiate(DispenseItem, _dispenseLocation, Quaternion.identity);
			GameObject g = sd.gameObject;
            var gLayeredController = sd.GetComponent<LayeredController>();
            gLayeredController.Layer = _dispenserLayeredController.Layer;
			Rigidbody rb = g.GetComponent<Rigidbody>();
			rb.AddForce((_dispenseLocation-transform.position)*DispenseSpeed);
			sd.Lifetime = DispenseItemLifetime;
		}
		Invoke("Dispense", DispenseDelay);
	}

	void ResumeDispensing()
	{
		Dispensing = true;
	}

	public override void Trigger ()
	{
		Dispensing = false;
		CancelInvoke("ResumeDispensing");
		Invoke("ResumeDispensing", TriggerDelay);
	}
}
