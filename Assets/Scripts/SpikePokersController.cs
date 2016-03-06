using UnityEngine;
using System.Collections;

public class SpikePokersController : DamageOnTouch
{
	private bool _isOut = false;
	private float _outTime = 2f;
	private float _maxY = 6f;
	private float _minY = 0f;
	private float _outRate = 1f;
	private float _inRate = .2f;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (_isOut && transform.localPosition.y < _maxY)
		{
			transform.localPosition = new Vector3(0, transform.localPosition.y + _outRate, 0);
		}
		if (!_isOut && transform.localPosition.y > _minY)
		{
			transform.localPosition = new Vector3(0, transform.localPosition.y - _inRate, 0);
		}
	}

	public void goOut()
	{
		if (transform.localPosition.y > _minY)
			return;
		_isOut = true;
		Invoke("goIn", _outTime);
	}

	public void goIn()
	{
		_isOut = false;
	}

}
