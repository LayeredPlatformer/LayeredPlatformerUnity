using UnityEngine;
using System.Collections;

public class TrapDoorSensor : MonoBehaviour
{
	public Triggerable Triggerable;

	private float _releaseTime = .5f;

	void OnTriggerEnter(Collider collider)
	{
		if (!collider.GetComponent<PlayerController>())
			return;
		Triggerable.Invoke("Trigger", _releaseTime);
	}
}
