using UnityEngine;
using System.Collections;

public class TimeAffected : LayeredController
{
	public bool isParent = true;
	public float UpdateDelaySeconds = 1;
	public GameObject Portal = null;

	protected bool CanUpdatePast = false;
	protected TimeAffected Shadow;
	protected bool ShadowBlinking = false;

	private int _counter = 0;
	private SpriteRenderer _rend;
	private Component[] _components = new Component[0];
	private Vector3[] _previousPositions;
	private float _shadowBlinkDuration = .1f;
	private float _shadowBlinkSlowAmount = .1f;
	private GameObject _shadowBlinkEffect;

	// Use this for initialization
	protected new void Initialize()
	{
		base.Initialize();
		_rend = GetComponent<SpriteRenderer>();
		_shadowBlinkEffect = (GameObject) Resources.Load("ShadowBlinkEffect");
		_previousPositions = new Vector3[(int)(60*UpdateDelaySeconds)];

		if (isParent)
		{
			Invoke("ToggleCanUpdatePast", UpdateDelaySeconds);
			var otherGO = (GameObject) Instantiate(gameObject, transform.position, transform.localRotation);
			Shadow = otherGO.GetComponent<TimeAffected>();
			Shadow.isParent = false;
			Shadow.Initialize();
			Shadow.ToggleReality();
		}
	}

	protected new void Step()
	{
		if (Portal != null)
			Portal.transform.position = transform.position;

		if (!isParent)
			return;

		base.Step();
		_previousPositions[_counter % (_previousPositions.Length)] = transform.position;
		_counter++;

		if (CanUpdatePast && !ShadowBlinking)
            Shadow.transform.position = _previousPositions[(_counter + _previousPositions.Length) % _previousPositions.Length];
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
            _rend.color = new Color(_rend.color.r, _rend.color.g, _rend.color.b, 1f);
        else
            _rend.color = new Color(_rend.color.r, _rend.color.g, _rend.color.b, .5f);
	}

    protected void ShadowBlink()
	{
		if (ShadowBlinking)
			return;
		Invoke("ShadowBlinkStart", _shadowBlinkDuration/2);
		Shadow.createPortal();
		createPortal();
		ShadowBlinking = true;
		SlowTime(_shadowBlinkSlowAmount, _shadowBlinkDuration/2);
	}

	private void ShadowBlinkStart()
	{
		transform.position = Shadow.transform.position;
        Layer = Layer.FindByZ(transform.position.z);
	}

	private void SlowTime(float amount, float duration)
	{
		Time.timeScale = amount;
		Time.fixedDeltaTime = .02f * amount;
		Invoke("RestoreTime", duration);
	}

	private void RestoreTime()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = .02f;
		ShadowBlinking = false;
		Destroy(Shadow.Portal);
	}

	public void createPortal()
	{
		Portal = (GameObject) Instantiate(_shadowBlinkEffect, transform.position, Quaternion.identity);
	}

}
