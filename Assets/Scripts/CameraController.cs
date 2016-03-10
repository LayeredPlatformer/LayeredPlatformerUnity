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
			Vector3 moved = Vector3.MoveTowards(transform.position, _panPos, _panRate*Time.deltaTime);
			transform.position = new Vector3(moved.x, moved.y, moved.z);
		}
	}

	public void pan(Vector3 pos, float seconds, Action callback = null)
	{
		_panning = true;
		_panPos = new Vector3(pos.x, pos.y, pos.z - DistanceFromPlayer);
		// R = D/T
		_panRate = Vector3.Distance(_panPos, transform.position)/(seconds);
		Invoke("stopPan", seconds);
        InvokeAction.Invoke(callback, seconds);
    }

	private void stopPan()
	{
		_panning = false;
	}

}
