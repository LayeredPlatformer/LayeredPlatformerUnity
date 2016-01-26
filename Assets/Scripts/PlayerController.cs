using UnityEngine;
using System.Collections;

public class PlayerController : TimeAffected
{
	// Use this for initialization
	public void Start()
	{
		base.Initialize();
        GetComponent<LayeredController>().LayerChanged += ((sender, args) => UpdateLayerTransparency());
	}
	
	// Update is called once per frame
	public void Update()
	{
		if (!isParent)
			return;

		base.Step();

		if (Input.GetKeyDown(KeyCode.F))
		{
			CycleLayers();
		}

		if (Input.GetKeyDown(KeyCode.S))
			ShadowBlink();
	}

	private void ShadowBlink()
	{
		transform.position = Shadow.transform.position;
		UpdateLayer(LayersEnum.ZToColor(transform.position.z));
	}

	private void UpdateLayerTransparency()
	{
		GameObject[] colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer");

		for (int i = 0; i < colorLayers.Length; i++)
		{
			LayeredController lc = colorLayers[i].GetComponent<LayeredController>();;
			if (lc._layer != _layer)
				SetGameObjectChildrenOpacity(colorLayers[i], 0.5f);
			else
				SetGameObjectChildrenOpacity(colorLayers[i], 1f);
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
