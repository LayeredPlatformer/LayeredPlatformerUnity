using UnityEngine;
using System.Collections;

public class SpikeEmitterController : MonoBehaviour
{
	public Triggerable Triggerable;

	private float _reactionDelay = 1f;

    void OnTriggerStay(Collider collider)
    {
		Triggerable.Invoke("Trigger", _reactionDelay);
    }
}
