using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody rb;
	Animator animator;
	SpriteRenderer sr;
	float _movementSpeed = 50f;
	float _jumpSpeed = 1600f;
	float _floatForce = 40f;
	float _floatDistance = 2f;
	float _jumpDistance= 2.5f;
	bool _canJump = true;
	float _jumpCooldown = 1f;

	enum states {idle, forward};

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		animator.SetInteger("state", (int) states.idle);
		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(-transform.right*_movementSpeed);
			animator.SetInteger("state", (int) states.forward);
			sr.flipX = true;
		}
		if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(transform.right*_movementSpeed);
			animator.SetInteger("state", (int) states.forward);
			sr.flipX = false;
		}
		if (Input.GetKeyDown(KeyCode.W) && Physics.Raycast(transform.position, -transform.up, _jumpDistance) && _canJump) 
		{
			rb.AddForce(transform.up*_jumpSpeed);
			_canJump = false;
			Invoke("restoreCanJump", _jumpCooldown);
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, _floatDistance)) 
		{
			float distance = (transform.position - hit.point).magnitude;
			rb.AddForce(transform.up*_floatForce/(Mathf.Sqrt(distance)));
		}
	}

	void restoreCanJump()
	{
		_canJump = true;
	}
}
