using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject Player;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
//		Debug.Log("calling update");
		transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
	}

}
