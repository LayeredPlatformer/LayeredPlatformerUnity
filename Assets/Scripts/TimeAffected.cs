using UnityEngine;
using System.Collections;

public class TimeAffected : LayeredController
{
	public bool isParent = true;

	protected bool CanUpdatePast = false;
	protected TimeAffected Shadow;

	private int _updateDelay = 1;
	private int _counter = 0;
	private SpriteRenderer _rend;
	private Component[] _components = new Component[0];
	private Vector3[] _previousPositions;

	// Use this for initialization
	protected new void Initialize()
	{
		base.Initialize();
		_rend = GetComponent<SpriteRenderer>();
		_previousPositions = new Vector3[60*_updateDelay];

		if (isParent)
		{
			Invoke("ToggleCanUpdatePast", _updateDelay);
			var otherGO = (GameObject) Instantiate(gameObject, transform.position, transform.localRotation);
			Shadow = otherGO.GetComponent<TimeAffected>();
			Shadow.isParent = false;
			Shadow.Initialize();
			Shadow.ToggleReality();
		}
	}

	protected new void Step()
	{
		base.Step();
		_previousPositions[_counter % (_previousPositions.Length)] = transform.position;
		_counter++;

        if (CanUpdatePast)
        {
            Shadow.transform.position = _previousPositions[(_counter + _previousPositions.Length) % _previousPositions.Length];
        }
	}
	
	public void ToggleCanUpdatePast()
	{
		CanUpdatePast = !CanUpdatePast;
	}

	public void ToggleReality()
	{
		if (_components.Length == 0)
		{
			_components = GetComponents<Component>();
			for (int i=0; i<_components.Length; i++)
			{
				if (!(_components[i] is Renderer || _components[i] is TimeAffected)
					|| _components[i] is Transform || _components[i] is PlatformCharacter3D)
				{
					Destroy(_components[i]);
//					Debug.Log("removed component: " + i);
				}
			}
		}
		ToggleOpacity();
	}

	public void ToggleOpacity()
	{
        if (_rend.color.a == .5f)
        {
            _rend.color = new Color(_rend.color.r, _rend.color.g, _rend.color.b, 1f);
        }
        else
        {
            _rend.color = new Color(_rend.color.r, _rend.color.g, _rend.color.b, .5f);
        }
	}
}
