using UnityEngine;
using System.Collections;
using System;

public class PlayerController : TimeAffected
{
	// Use this for initialization
	public void Start()
	{
        GetComponent<LayeredController>().LayerChanged += UpdateLayerTransparencyOnLayerChange;
        Initialize();
	}
	
	// Update is called once per frame
	public void Update()
	{
        if (!isParent)
        {
            return;
        }

        Step();

		if (Input.GetKeyDown(KeyCode.F))
		{
			CycleLayers();
		}

        if (Input.GetKeyDown(KeyCode.S))
        {
            ShadowBlink();
        }
	}

    private void CycleLayers()
    {
        if (Layer == LayersEnum.Colors.Red)
        {
            Layer = LayersEnum.Colors.Blue;
        }
        else if (Layer == LayersEnum.Colors.Blue)
        {
            Layer = LayersEnum.Colors.Green;
        }
        else if (Layer == LayersEnum.Colors.Green)
        {
            Layer = LayersEnum.Colors.Red;
        }
    }

    private void ShadowBlink()
	{
		transform.position = Shadow.transform.position;
		Layer = LayersEnum.ZToColor(transform.position.z);
	}

	private void UpdateLayerTransparencyOnLayerChange(object sender, EventArgs args)
	{
		var colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer");

		for (int i = 0; i < colorLayers.Length; i++)
		{
			var layeredController = colorLayers[i].GetComponent<LayeredController>();
            if (layeredController.Layer != Layer)
            {
                SetGameObjectChildrenOpacity(colorLayers[i], 0.5f);
            }
            else
            {
                SetGameObjectChildrenOpacity(colorLayers[i], 1f);
            }
		}
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
}
