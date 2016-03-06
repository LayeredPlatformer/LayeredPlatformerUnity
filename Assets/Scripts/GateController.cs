using UnityEngine;
using System.Collections;

public class GateController : Triggerable
{
	public float RetractDistance = 5f;
	public float RetractRate = .1f;
	public GateController._directions RetractDirection;

	public enum _directions {up, down, left, right};
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
				case _directions.up:
					t.y += RetractRate;
					break;
				case _directions.down:
					t.y -= RetractRate;
					break;
				case _directions.left:
					t.x -= RetractRate;
					break;
				case _directions.right:
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
