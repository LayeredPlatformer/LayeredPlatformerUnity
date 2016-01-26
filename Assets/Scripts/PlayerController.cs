using UnityEngine;
using System.Collections;

public class PlayerController : TimeAffected
{
	// Use this for initialization
	void Start ()
	{
		base.Initialize();
        GetComponent<LayeredController>().LayerChanged += ((sender, args) => updateLayerTransparency());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isParent)
			return;

		base.Step();

		if (Input.GetKeyDown(KeyCode.F))
		{
			CycleLayers();
		}

		if (Input.GetKeyDown(KeyCode.S))
			shadowBlink();
	}

	void shadowBlink()
	{
		transform.position = Shadow.transform.position;
		UpdateLayer(LayersEnum.zToColor(transform.position.z));
	}

	void updateLayerTransparency()
	{
		GameObject[] colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer");

		for (int i = 0; i < colorLayers.Length; i++)
		{
			LayeredController lc = colorLayers[i].GetComponent<LayeredController>();;
			if (lc._layer != _layer)
				setGameObjectChildrenOpacity(colorLayers[i], 0.5f);
			else
				setGameObjectChildrenOpacity(colorLayers[i], 1f);
		}
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
