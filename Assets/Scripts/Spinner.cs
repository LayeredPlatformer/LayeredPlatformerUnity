using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public float rotSpeed = 1;

	private Vector3 _rotation = Vector3.zero;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		Rotate();
	}

	void Rotate()
	{
		_rotation = new Vector3(0,0,_rotation.z-rotSpeed*Time.fixedDeltaTime*Utility.getFPS());
		transform.localRotation = Quaternion.Euler(_rotation);
	}
}
