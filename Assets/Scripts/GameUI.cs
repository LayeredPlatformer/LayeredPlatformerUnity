using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
	PlayerController _player;
	Targetable _playerTargetable;
	Texture _oilScreen;
	Texture _shadowBlinkIcon;
	Texture _layerJumpIcon;
	const float _buttonWidth = 64f;
	const float _buttonHeight = _buttonWidth;
	Rect _shadowBlinkRect = new Rect(Screen.width-_buttonWidth, Screen.height-_buttonHeight, _buttonWidth, _buttonHeight);
	Rect _layerJumpRect = new Rect(Screen.width-_buttonWidth*2, Screen.height-_buttonHeight, _buttonWidth, _buttonHeight);
	bool _shadowBlinkPressed = false;
	bool _layerJumpPressed = false;
	float _buttonPressedDuration = .1f;

	void Start ()
	{
		_player = GameObject.Find ("Player").GetComponent<PlayerController>();
		_playerTargetable = _player.GetComponent<Targetable> ();
		_oilScreen = (Texture)Resources.Load ("OilScreen");
		_shadowBlinkIcon = (Texture)Resources.Load ("shadowBlinkIcon");
		_layerJumpIcon = (Texture)Resources.Load ("layerJumpIcon");
	}

	void resetLayerJumpPressed()
	{
		_layerJumpPressed = false;
	}

	void resetShadowBlinkPressed()
	{
		_shadowBlinkPressed = false;
	}

	void OnGUI ()
	{
		// oil screen
		Color temp = GUI.color;
		temp.a = 1 - (_playerTargetable.Health / _playerTargetable.MaxHealth);
		GUI.color = temp;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _oilScreen);

		// player controls
		if (GUI.Button(_shadowBlinkRect, ""))
		{
			_player.InitiateShadowBlink();
			_shadowBlinkPressed = true;
			CancelInvoke("resetShadowBlinkPressed");
			Invoke("resetShadowBlinkPressed", _buttonPressedDuration);
		}
		if (GUI.Button(_layerJumpRect, ""))
		{
			_player.InitiateLayerJump();
			_layerJumpPressed = true;
			CancelInvoke("resetLayerJumpPressed");
			Invoke("resetLayerJumpPressed", _buttonPressedDuration);
		}


		if (_shadowBlinkPressed)
			GUI.color = Color.gray;
		else
			GUI.color = Color.white;
		GUI.DrawTexture(_shadowBlinkRect, _shadowBlinkIcon);
			
		if (_layerJumpPressed)
			GUI.color = Color.gray;
		else
			GUI.color = Color.white;
		GUI.DrawTexture(_layerJumpRect, _layerJumpIcon);
	}
}
