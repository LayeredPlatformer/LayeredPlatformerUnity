using UnityEngine;
using System.Collections;

public class GateController : Triggerable
{
	public float RetractDistance = 5f;
	public float RetractRate = .1f;
	public Utility.Directions RetractDirection;

	private bool _retracting = false;
	private float _distanceRetracted = 0;

	void Update ()
	{
		if (_retracting && _distanceRetracted < RetractDistance)
		{
			_distanceRetracted += RetractRate;
			Vector3 t = transform.position;
			switch(RetractDirection)
			{
				case Utility.Directions.up:
					t.y += RetractRate;
					break;
				case Utility.Directions.down:
					t.y -= RetractRate;
					break;
				case Utility.Directions.left:
					t.x -= RetractRate;
					break;
				case Utility.Directions.right:
					t.x += RetractRate;
					break;
			}
			transform.position = t;
		}
	}

	public override void Trigger()
	{
		_retracting = true;
	}

}
