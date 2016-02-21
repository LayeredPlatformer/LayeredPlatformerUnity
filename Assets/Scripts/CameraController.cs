using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject Player;

	private bool _panning = false;
	private Vector3 _panPos;
	private float _panRate;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!_panning)
			transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
		else
		{
			float oldZ = transform.position.z;
			Vector3 moved = Vector3.MoveTowards(transform.position, _panPos, _panRate*Time.deltaTime);
			transform.position = new Vector3(moved.x, moved.y, oldZ);
		}
	}

	public void pan(Vector3 pos, float seconds)
	{
		_panning = true;
		_panPos = pos;
		// R = D/T
		_panRate = Vector3.Distance(pos, transform.position)/(seconds/3);
		//TODO: idk why my equations for pan rate don't work: 3 is a magic number that fixes it
		Invoke("stopPan", seconds);
	}

	private void stopPan()
	{
		_panning = false;
	}

}
