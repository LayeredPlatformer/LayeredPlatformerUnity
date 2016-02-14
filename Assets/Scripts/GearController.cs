using UnityEngine;
using System.Collections;

public class GearController : LayeredController
{
	public PlayerController Player;
	public float RotationSpeed = 1f;
	public float Damage = 1;

	private bool _throwing = false;
	private bool _returning = false;
	private Rigidbody _rb;
	private float _throwSpeed;
	private Vector3 _throwTarget;
	private Vector3 _rotation = Vector3.zero;
	private int throwRotationIncrease = 5;
	private float _forceAmplifier = 100f;

	// Use this for initialization
	void Start()
	{
		base.Initialize();
		_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update()
	{
		base.Step();
		Rotate();
		if (_throwing)
			ThrowMove();
		else if (_returning)
			ReturnThrowMove();
		else
			transform.position = Player.transform.position;
	}

	void Rotate()
	{
		float rotAmount;
		if (_throwing)
			rotAmount = RotationSpeed * throwRotationIncrease;
		else
			rotAmount = RotationSpeed;
		_rotation = new Vector3(0,0,_rotation.z-rotAmount*Time.fixedDeltaTime*Utility.getFPS());
		transform.localRotation = Quaternion.Euler(_rotation);
	}

	public void Throw(Vector3 target, float speed, float travelTime)
	{
		if (_throwing || _returning)
			return;
		_throwing = true;
		_throwSpeed = speed;
		_throwTarget = target;
		Invoke("StartReturnThrow", travelTime);
	}

	public void StartReturnThrow()
	{
		_throwing = false;
		_returning = true;
	}

	private void ThrowMove()
	{
		transform.position = Vector3.MoveTowards(transform.position, _throwTarget, _throwSpeed*Time.fixedDeltaTime*Utility.getFPS());
	}

	private void ReturnThrowMove()
	{
		transform.position = Vector3.MoveTowards(transform.position, Player.transform.position,
			_throwSpeed*Time.fixedDeltaTime*Utility.getFPS());
		if (transform.position == Player.transform.position)
			_returning = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		if ((!_returning && !_throwing) || collider.GetComponent<PlayerController>())
			return;
		Targetable targetable = collider.GetComponent<Targetable>();
		if (targetable)
		{
			targetable.DealDamage(Damage, transform.position, Damage*_forceAmplifier);
			Debug.Log("health: " + targetable.GetHealth());
		}
	}

}
