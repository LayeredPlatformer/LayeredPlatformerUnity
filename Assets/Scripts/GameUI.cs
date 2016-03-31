using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
	PlayerController _player;
	Targetable _playerTargetable;
	Texture _oilScreen;
	const float _buttonWidth = 64f;
	const float _buttonHeight = _buttonWidth;
	Rect _shadowBlinkRect = new Rect(Screen.width-_buttonWidth, Screen.height-_buttonHeight, _buttonWidth, _buttonHeight);
	Rect _layerJumpRect = new Rect(Screen.width-_buttonWidth*2, Screen.height-_buttonHeight, _buttonWidth, _buttonHeight);

	void Start ()
	{
		_player = GameObject.Find ("Player").GetComponent<PlayerController>();
		_playerTargetable = _player.GetComponent<Targetable> ();
		_oilScreen = (Texture)Resources.Load ("OilScreen");
	}

	void OnGUI ()
	{
		// oil screen
		Color temp = GUI.color;
		temp.a = 1 - (_playerTargetable.Health / _playerTargetable.MaxHealth);
		GUI.color = temp;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _oilScreen);

		GUI.color = Color.white;
		// player controls
		if (GUI.Button(_shadowBlinkRect, ""))
			_player.InitiateShadowBlink();
		if (GUI.Button(_layerJumpRect, ""))
			_player.InitiateLayerJump();
	}
}
