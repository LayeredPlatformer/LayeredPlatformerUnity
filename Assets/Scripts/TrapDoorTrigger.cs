using UnityEngine;
using System.Collections;

public class TrapDoorTrigger : MonoBehaviour
{
	public Rigidbody trapDoorRigidBody;

	private float _releaseTime = .5f;

	void OnTriggerEnter(Collider collider)
	{
		if (!collider.GetComponent<PlayerController>())
			return;
		Invoke("ReleaseDoor", _releaseTime);
	}

	void ReleaseDoor()
	{
		trapDoorRigidBody.isKinematic = false;
	}
}
