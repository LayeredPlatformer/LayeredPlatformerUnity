using UnityEngine;
using System.Collections;

public class PlayerController : TimeAffected
{

	public static int zone = 1;
    public const float LayerDifference = 3;

	bool layerToggle = false;
	int currentLayer = 0;
    const int MaxLayer = 2;

	// Use this for initialization
	void Start ()
	{
		base.initialize();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isParent)
			return;

		base.step();

		if (Input.GetKeyDown(KeyCode.F))
			cycleLayers();

		if (Input.GetKeyDown(KeyCode.S))
			shadowBlink();
	}

	void shadowBlink()
	{
		transform.position = shadow.transform.position;
	}

	void cycleLayers()
	{
		currentLayer++;
		transform.Translate(0, 0, LayerDifference);

		if (currentLayer > MaxLayer)
		{
			transform.Translate(0, 0, -1 * (LayerDifference * (MaxLayer + 1)));
			currentLayer = 0;
		}

		GameObject[] colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer");

		for (int i = 0; i < colorLayers.Length; i++)
		{
			if (i < currentLayer)
			{
				setGameObjectChildrenOpacity(colorLayers[i], 0.5f);
			}
			else
			{
				setGameObjectChildrenOpacity(colorLayers[i], 1f);
			}
		}

		layerToggle = false;
	}

	private static void setGameObjectChildrenOpacity(GameObject gameObject, float opacity)
	{
		var renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (var renderer in renderers)
		{
			Color layerColor = renderer.material.color;
			layerColor.a = opacity;
			renderer.material.color = layerColor;
		}
	}
}
