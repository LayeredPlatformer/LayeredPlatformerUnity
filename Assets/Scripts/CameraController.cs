using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
	GameObject Player;

	private bool _isPanning = false;
	private Vector3 _panDestination;
	private float _panRate;
	private Action _panCallback;

	public float DistanceFromPlayer = 10;
	public float PanCompletionThreshold = 0.5f;

	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_isPanning)
		{
			transform.position = Vector3.MoveTowards (transform.position, _panDestination, _panRate * Time.deltaTime);
			if (Vector3.Distance (transform.position, _panDestination) < PanCompletionThreshold)
			{
				_isPanning = false;
				if (_panCallback != null)
					_panCallback ();
			}
		} else
			transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - DistanceFromPlayer);
	}

	public void PanByTime (Vector3 destination, float seconds, Action callback = null)
	{
		if (_isPanning)
			return;

		_isPanning = true;
		_panDestination = new Vector3 (destination.x, destination.y, destination.z - DistanceFromPlayer);
		// R = D/T
		_panRate = Vector3.Distance (_panDestination, transform.position) / (seconds);
		_panCallback = callback;
	}

	public void PanByRate (Vector3 destination, float panRate, Action callback = null)
	{
		if (_isPanning)
			return;

		_isPanning = true;
		_panDestination = new Vector3 (destination.x, destination.y, destination.z - DistanceFromPlayer);
		_panRate = panRate;
		_panCallback = callback;
	}
}
