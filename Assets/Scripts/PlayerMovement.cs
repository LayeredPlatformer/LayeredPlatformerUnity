using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody _rb;
	Animator _animator;
	SpriteRenderer _sr;
	float _movementSpeed = 50f;
	float _jumpSpeed = 1600f;
	float _floatForce = 50f;
	float _floatDistance = 2f;
	float _jumpDistance= 2.5f;
	bool _canJump = true;
	float _jumpCooldown = 1f;
	float _movementDistance = 1f; // how far from walls you need to be in order to move towards them
	float _playerHeight;
	float _playerWidth;

	const int _obstacleLayer = 9;
	const int _obstacleLayerMask = 1 << _obstacleLayer;

	enum states {idle, forward};

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
		_sr = GetComponent<SpriteRenderer>();
		_playerHeight = GetComponent<BoxCollider>().size.y*transform.localScale.y;
		_playerWidth = GetComponent<BoxCollider>().size.x*transform.localScale.x;
	}

	void Update()
	{
		_animator.SetInteger("state", (int) states.idle);

		// left move
		if (Input.GetKey(KeyCode.A))
		{
			if (leftClear() || !bottomClear(_jumpDistance))
			{
				_rb.AddForce(-transform.right*_movementSpeed);
				_animator.SetInteger("state", (int) states.forward);
				_sr.flipX = true;
			}
		}

		// right move
		if (Input.GetKey(KeyCode.D))
		{
			if (rightClear() || !bottomClear(_jumpDistance))
			{
				_rb.AddForce(transform.right*_movementSpeed);
				_animator.SetInteger("state", (int) states.forward);
				_sr.flipX = false;
			}
		}

		// jump
		if (_canJump && Input.GetKeyDown(KeyCode.W)) 
		{
			if (!bottomClear(_jumpDistance))
			{
				_rb.AddForce(transform.up*_jumpSpeed);
				_canJump = false;
				Invoke("restoreCanJump", _jumpCooldown);
			}
		}

		// float
		if (!bottomClear(_floatDistance))
			_rb.AddForce(transform.up*_floatForce);
	}

	bool leftClear()
	{
		Vector3 leftPosUpper = new Vector3(transform.position.x-_movementDistance,
			transform.position.y+_playerHeight/2, transform.position.z);
		Vector3 leftPosLower = new Vector3(transform.position.x-_movementDistance,
			transform.position.y-_playerHeight/2, transform.position.z);
		return !Physics.Raycast(leftPosUpper, -transform.up, _playerHeight, _obstacleLayerMask)
			&& !Physics.Raycast(leftPosLower, transform.up, _playerHeight, _obstacleLayerMask);
	}

	bool rightClear()
	{
		Vector3 rightPosUpper = new Vector3(transform.position.x+_movementDistance,
			transform.position.y+_playerHeight/2, transform.position.z);
		Vector3 rightPosLower = new Vector3(transform.position.x+_movementDistance,
			transform.position.y-_playerHeight/2, transform.position.z);
		return !Physics.Raycast(rightPosUpper, -transform.up, _playerHeight, _obstacleLayerMask)
			&& !Physics.Raycast(rightPosLower, transform.up, _playerHeight, _obstacleLayerMask);
	}


	bool bottomClear(float amount)
	{
		Vector3 bottomPosLeft2 = new Vector3(transform.position.x-_playerWidth/2,
			transform.position.y, transform.position.z);
		Vector3 bottomPosRight2 = new Vector3(transform.position.x+_playerWidth/2,
			transform.position.y, transform.position.z);
		return !Physics.Raycast(bottomPosLeft2, -transform.up, amount, _obstacleLayerMask)
			&& !Physics.Raycast(bottomPosRight2, -transform.up, amount, _obstacleLayerMask);
	}

	void restoreCanJump()
	{
		_canJump = true;
	}
}
