using UnityEngine;
using System.Collections;

public class TrapDoorBody : Triggerable
{
	public override void Trigger()
	{
		GetComponent<Rigidbody>().isKinematic = false;
	}
}
