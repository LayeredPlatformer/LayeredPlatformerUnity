using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody rb;
	float _movementSpeed = 50f;
	float _jumpSpeed = 1600f;
	float _floatForce = 40f;
	float _floatDistance = 2f;
	float _jumpDistance= 2.5f;
	bool _canJump = true;
	float _jumpCooldown = 1f;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.A))
			rb.AddForce(-transform.right*_movementSpeed);
		if (Input.GetKey(KeyCode.D))
			rb.AddForce(transform.right*_movementSpeed);
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
