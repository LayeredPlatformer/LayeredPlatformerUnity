using UnityEngine;
using System.Collections;
using System;

public class TimeAffected : LayeredController
{
    public event EventHandler ShadowMetParentHandler;
    public event EventHandler ShadowBlinkHandler;

	public bool isParent = true;
	public float UpdateDelaySeconds = 1;
	public GameObject Portal = null;
    public AudioClip ShadowBlinkSound;
	public TimeAffected Shadow;

	protected bool CanUpdatePast = false;
	protected bool ShadowBlinking = false;

	private int _counter = 0;
	private SpriteRenderer _rend;
	private Component[] _components = new Component[0];
	private Vector3[] _previousPositions;
	private GameObject _shadowBlinkEffect;
	private float _shadowBlinkFirstHalfDuration = .04f;
	private float _shadowBlinkSecondHalfDuration = .06f;
	private float _shadowBlinkSlowAmount = .2f;

    public bool ShadowAtParent
    {
        get
        {
            if (isParent)
                return Shadow.transform.position == transform.position;
            else
                return false;
        }
    }

	void Start()
	{
		Initialize();
	}

	void Update()
	{
		Step();
	}

	// Use this for initialization
	protected new void Initialize()
	{
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
		base.Initialize();
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

        bool shadowAlreadyAtParent = ShadowAtParent;

        if (CanUpdatePast && !ShadowBlinking)
        {
            Shadow.transform.position = _previousPositions[(_counter + _previousPositions.Length) % _previousPositions.Length];

            if (!shadowAlreadyAtParent && ShadowAtParent)
                OnShadowMetParent();
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
				if (!(_components[i] is Renderer || _components[i] is TimeAffected
					|| _components[i] is Transform || _components[i] is PlatformCharacter3D))
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

    public void ShadowBlink()
	{
		if (ShadowBlinking)
			return;

        AudioSource.PlayClipAtPoint(ShadowBlinkSound, transform.position, 10);
		Invoke("ShadowBlinkStart", _shadowBlinkFirstHalfDuration);
		SlowTime(_shadowBlinkSlowAmount, _shadowBlinkFirstHalfDuration+_shadowBlinkSecondHalfDuration);
		Shadow.createPortal();
		createPortal();
		ShadowBlinking = true;

        OnShadowBlink();
	}

    protected virtual void OnShadowMetParent()
    {
        Debug.Log("Shadow met parent");
        if (ShadowMetParentHandler != null)
        {
            ShadowMetParentHandler(this, new EventArgs());
        }
    }

    private void OnShadowBlink()
    {
        if (ShadowBlinkHandler != null)
        {
            ShadowBlinkHandler(this, new ShadowBlinkEventArgs { IsShadowBlinking = ShadowBlinking });
        }
    }

    private void ShadowBlinkStart()
	{
		transform.position = Shadow.transform.position;
        Layer = Layer.FindByZ(transform.position.z);
	}

	private void SlowTime(float amount, float duration)
	{
		Invoke("RestoreTime", duration);
		Time.timeScale = amount;
		Time.fixedDeltaTime = .02f * amount;
	}

	private void RestoreTime()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = .02f;
		ShadowBlinking = false;
		Destroy(Shadow.Portal);
        OnShadowBlink();
    }

	public void createPortal()
	{
		Portal = (GameObject) Instantiate(_shadowBlinkEffect, transform.position, Quaternion.identity);
	}

	public float getShadowBlinkDuration()
	{
		return _shadowBlinkFirstHalfDuration;
	}

}

public class ShadowBlinkEventArgs : EventArgs
{
    public bool IsShadowBlinking { get; set; }
}
