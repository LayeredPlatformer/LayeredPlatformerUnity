using UnityEngine;
using System.Collections;

public class PlayerController : TimeAffected
{

	public static int zone = 1;
	private bool layerToggle = false;
	private int currentLayer = 0;
    private const int MaxLayer = 2;
    public const float LayerDifference = 3;


	// Use this for initialization
	void Start ()
	{
		base.initialize();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.step();

		if (Input.GetKeyDown(KeyCode.F))
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
					SetGameObjectChildrenOpacity(colorLayers[i], 0.5f);
				}
				else
				{
					SetGameObjectChildrenOpacity(colorLayers[i], 1f);
				}
			}


			layerToggle = false;
		}
	}

	private static void SetGameObjectChildrenOpacity(GameObject gameObject, float opacity)
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
