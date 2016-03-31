using UnityEngine;
using System.Collections;

public class PlayerUI: MonoBehaviour
{
	PlayerController _player;
	Targetable _playerTargetable;
	Texture _oilScreen;
	Texture _shadowBlinkIcon;
	Texture _layerJumpIcon;
	Texture _joyStickIcon;
	Texture _joyStickPadIcon;
	const float _buttonWidth = 64f;
	const float _buttonHeight = _buttonWidth;
	const float _buttonPressedDuration = .1f;
	const float _joyStickBuffer = 128f;
	const float _joyStickSize = 64f;
	const float _joyStickPadSize = _joyStickSize + _joyStickRange;
	const float _joyStickActionThreshold = 20f;
	Rect _shadowBlinkRect = new Rect (Screen.width - _buttonWidth, Screen.height - _buttonHeight, _buttonWidth, _buttonHeight);
	Rect _layerJumpRect = new Rect (Screen.width - _buttonWidth * 2, Screen.height - _buttonHeight, _buttonWidth, _buttonHeight);
	Rect _joyStickPadRect;
	bool _shadowBlinkPressed = false;
	bool _layerJumpPressed = false;
	Vector2 _joyStickOrigin = new Vector3 (_joyStickBuffer, Screen.height - _joyStickBuffer);
	bool _draggingJoyStick = false;
	PlayerMovement _playerMovement;

	public const float _joyStickRange = 32;
	public Vector2 _joyStickDragOffset = Vector2.zero;

	void Start ()
	{
		_player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		_playerTargetable = _player.GetComponent<Targetable> ();
		_playerMovement = _player.GetComponent<PlayerMovement> ();
		_oilScreen = (Texture)Resources.Load ("OilScreen");
		_shadowBlinkIcon = (Texture)Resources.Load ("ShadowBlinkIcon");
		_layerJumpIcon = (Texture)Resources.Load ("LayerJumpIcon");
		_joyStickIcon = (Texture)Resources.Load ("JoyStick");
		_joyStickPadIcon = (Texture)Resources.Load ("JoyStickPad");

		_joyStickPadRect = new Rect (_joyStickOrigin.x - _joyStickPadSize / 2, _joyStickOrigin.y - _joyStickPadSize / 2,
			_joyStickPadSize, _joyStickPadSize);
	}

	void Update ()
	{
		Vector2 mousePos = (Vector2)Input.mousePosition;
		mousePos.y = Screen.height - mousePos.y;

		if (Input.GetMouseButtonDown (0))
		{
			if (Vector2.Distance (mousePos, _joyStickOrigin) < _joyStickRange)
				_draggingJoyStick = true;
		}

		if (Input.GetMouseButtonUp (0))
		{
			_draggingJoyStick = false;
			_joyStickDragOffset = Vector2.zero;
		}

		if (_draggingJoyStick)
		{
			_joyStickDragOffset = mousePos - _joyStickOrigin;
			if (_joyStickDragOffset.magnitude > _joyStickRange)
				_joyStickDragOffset = _joyStickDragOffset.normalized * _joyStickRange;
			movePlayerWithJoyStick ();
		} else if (!_player.PauseController.isPaused)
		{
			if (Input.GetMouseButtonDown (1))
				_player.ThrowBigGear ();
			if (Input.GetMouseButtonDown (0))
				_player.ThrowSmallGear ();
			if (Input.GetKeyDown (KeyCode.S))
				pressShadowBlink ();
			if (Input.GetKeyDown (KeyCode.F))
				pressLayerJump ();

			// simulate the joystick when using keyboard movement
			if (Input.GetKey (KeyCode.A))
				moveJoyStickLeft ();
			else if (Input.GetKey (KeyCode.D))
				moveJoyStickRight ();
			else
				resetHorJoyStick ();
			
			if (Input.GetKey (KeyCode.W))
				moveJoyStickUp ();
			else
				resetVertJoyStick ();
		}

	}

	void movePlayerWithJoyStick ()
	{
		if (_joyStickDragOffset.x < -_joyStickActionThreshold)
			_playerMovement.moveLeft ();
		if (_joyStickDragOffset.x > _joyStickActionThreshold)
			_playerMovement.moveRight ();
		if (_joyStickDragOffset.y < -_joyStickActionThreshold)
			_playerMovement.jump ();
	}

	void moveJoyStickLeft ()
	{
		_joyStickDragOffset.x = -_joyStickRange;
	}

	void moveJoyStickRight ()
	{
		_joyStickDragOffset.x = _joyStickRange;
	}

	void moveJoyStickUp ()
	{
		_joyStickDragOffset.y = -_joyStickRange;
	}

	void resetHorJoyStick ()
	{
		_joyStickDragOffset.x = 0;
	}

	void resetVertJoyStick ()
	{
		_joyStickDragOffset.y = 0;
	}

	void resetLayerJumpPressed ()
	{
		_layerJumpPressed = false;
	}

	void resetShadowBlinkPressed ()
	{
		_shadowBlinkPressed = false;
	}

	void pressShadowBlink ()
	{
		_player.InitiateShadowBlink ();
		_shadowBlinkPressed = true;
		CancelInvoke ("resetShadowBlinkPressed");
		Invoke ("resetShadowBlinkPressed", _buttonPressedDuration);
	}

	void pressLayerJump ()
	{
		_player.InitiateLayerJump ();
		_layerJumpPressed = true;
		CancelInvoke ("resetLayerJumpPressed");
		Invoke ("resetLayerJumpPressed", _buttonPressedDuration);
	}

	void OnGUI ()
	{
		// oil screen
		Color temp = GUI.color;
		temp.a = 1 - (_playerTargetable.Health / _playerTargetable.MaxHealth);
		GUI.color = temp;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _oilScreen);

		// Button press
		if (GUI.Button (_shadowBlinkRect, ""))
		{
			pressShadowBlink ();
		}
		if (GUI.Button (_layerJumpRect, ""))
		{
			pressLayerJump ();
		}

		// Button draw
		if (_shadowBlinkPressed)
			GUI.color = Color.gray;
		else
			GUI.color = Color.white;
		GUI.DrawTexture (_shadowBlinkRect, _shadowBlinkIcon);
		if (_layerJumpPressed)
			GUI.color = Color.gray;
		else
			GUI.color = Color.white;
		GUI.DrawTexture (_layerJumpRect, _layerJumpIcon);

		// Joystick
		GUI.color = Color.white;
		GUI.DrawTexture (_joyStickPadRect, _joyStickPadIcon);
		Rect joyStickRect = new Rect (_joyStickOrigin.x - _joyStickSize / 2 + _joyStickDragOffset.x,
			                    _joyStickOrigin.y - _joyStickSize / 2 + _joyStickDragOffset.y, _joyStickSize, _joyStickSize);
		GUI.DrawTexture (joyStickRect, _joyStickIcon);
	}
}
