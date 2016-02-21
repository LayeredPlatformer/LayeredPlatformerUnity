using UnityEngine;
using System.Collections;

public class SpikeEmitterController : MonoBehaviour
{
	public GameObject SpikePokersGameObject;

	private SpikePokersController _spikePokers;
	private float _reactionDelay = 1f;

	void Start ()
	{
		_spikePokers = SpikePokersGameObject.GetComponent<SpikePokersController>();
	}
	
	void Update ()
	{
	
	}

    void OnTriggerStay(Collider collider)
    {
		_spikePokers.Invoke("goOut", _reactionDelay);
    }
}
