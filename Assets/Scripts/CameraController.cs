using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
	public GameObject Player;

	private bool _panning = false;
	private Vector3 _panPos;
	private float _panRate;

    public float DistanceFromPlayer = 10;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!_panning)
			transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - DistanceFromPlayer);
		else
		{
			float oldZ = transform.position.z;
			Vector3 moved = Vector3.MoveTowards(transform.position, _panPos, _panRate*Time.deltaTime);
			transform.position = new Vector3(moved.x, moved.y, oldZ);
		}
	}

	public void pan(Vector3 pos, float seconds, Action callback = null)
	{
		_panning = true;
		_panPos = pos;
		// R = D/T
		_panRate = Vector3.Distance(pos, transform.position) * 3/(seconds);
		//TODO: idk why my equations for pan rate don't work: 3 is a magic number that fixes it
		Invoke("stopPan", seconds);
        InvokeAction.Invoke(callback, seconds);
    }

	private void stopPan()
	{
		_panning = false;
	}

}
