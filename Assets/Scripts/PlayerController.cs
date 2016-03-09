using UnityEngine;
using System.Collections;
using System;

public class PlayerController : TimeAffected
{
	private GearController _bigGear;
	private GearController _smallGear;
	private GameObject _bigGearPrefab;
	private GameObject _smallGearPrefab;
	private CameraController _camera;
	private Targetable _targetable;
    private MusicController _musicController;

	private float _bigGearSpeed = .6f;
	private float _smallGearSpeed = .3f;
	private float _bigGearTravelTime = .2f;
	private float _smallGearTravelTime = .3f;
	private float _bigGearDefaultRotationSpeed = 1f;
	private float _smallGearDefaultRotationSpeed = 2f;
	private float _bigGearMaxDist = 2f;
	private float _smallGearDamage = 1f;
	private float _bigGearDamage = 3f;

	private Texture _oilScreen;

	// Use this for initialization
	public void Start()
	{
        if (!isParent)
            return;

        Initialize();

		_targetable = gameObject.GetComponent<Targetable>();
        _targetable.DeathEventHandler += OnDeath;
		_camera = Camera.main.GetComponent<CameraController>();
        _musicController = gameObject.GetComponent<MusicController>();
        GetComponent<LayeredController>().LayerChangedEventHandler += UpdateLayerTransparencyOnLayerChange;
        GetComponent<LayeredController>().LayerChangedEventHandler += UpdateMusicOnLayerChange;

		_oilScreen = (Texture) Resources.Load("OilScreen");
		_bigGearPrefab = (GameObject) Resources.Load("BigGear");
		_smallGearPrefab = (GameObject) Resources.Load("SmallGear");
		_bigGear = Instantiate(_bigGearPrefab).GetComponent<GearController>();
		_smallGear = Instantiate(_smallGearPrefab).GetComponent<GearController>();
		_bigGear.Player = this;
		_smallGear.Player = this;
		_bigGear.RotationSpeed = _bigGearDefaultRotationSpeed;
		_smallGear.RotationSpeed = _smallGearDefaultRotationSpeed;
		_bigGear.Damage = _bigGearDamage;
		_smallGear.Damage = _smallGearDamage;
	}
	
	// Update is called once per frame
	public void Update()
	{
        base.Step();

        if (!isParent)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            InitiateLayerStep();

		if (Input.GetKeyDown(KeyCode.S))
            InitiateShadowBlink();
        

		if (Input.GetMouseButtonDown(1))
		{
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(
				new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z+Layer.Z));
			_smallGear.Throw(worldPos, _smallGearSpeed, _smallGearTravelTime);
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(
				new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z+Layer.Z));
			Ray dir = new Ray(transform.position, worldPos - transform.position);
			Vector3 attackPos = dir.GetPoint(_bigGearMaxDist);
			_bigGear.Throw(attackPos, _bigGearSpeed, _bigGearTravelTime);
		}
	}

    public void InitiateLayerStep()
    {
        Layer++;
    }

    public void InitiateShadowBlink()
    {
        _camera.pan(Shadow.transform.position, Shadow.getShadowBlinkDuration());
        ShadowBlink();
    }

	private void UpdateLayerTransparencyOnLayerChange(object sender, EventArgs args)
	{
		var colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer");

		for (int i = 0; i < colorLayers.Length; i++)
		{
			var layeredController = colorLayers[i].GetComponent<LayeredController>();
			var opacity = layeredController.Layer == Layer ? 1f : 0.5f;
			SetGameObjectChildrenOpacity(colorLayers[i], opacity);
		}
	}

	private void UpdateMusicOnLayerChange(object sender, EventArgs args)
	{
        var layerChangedArgs = (LayerChangedEventArgs)args;
        _musicController.LayerChange(Layer.Index);
		// change game music
		Debug.Log("change the music!");
	}

	private static void SetGameObjectChildrenOpacity(GameObject gameObject, float opacity)
	{
		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (var renderer in renderers)
		{
			var layerColor = renderer.material.color;
			layerColor.a = opacity;
			renderer.material.color = layerColor;
		}
	}

    private void OnDeath(object sender, EventArgs args)
    {
        Debug.Log("Player died");
        _targetable.DeathEventHandler -= OnDeath;
        ShadowMetParentHandler += ShadowMetParentAfterDeath;
    }

    private void ShadowMetParentAfterDeath(object sender, EventArgs args)
    {
        Debug.Log("After death, shadow met parent");
        ShadowMetParentHandler -= ShadowMetParentAfterDeath;
        // TODO send player back to checkpoint
        _targetable.DeathEventHandler += OnDeath;
    }

    void OnGUI()
	{
        if (!isParent)
            return;
		Color temp = GUI.color;
		temp.a = 1-(_targetable.Health/_targetable.MaxHealth);
		GUI.color = temp;
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), _oilScreen);
	}

}