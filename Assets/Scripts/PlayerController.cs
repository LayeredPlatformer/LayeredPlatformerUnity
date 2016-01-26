using UnityEngine;
using System.Collections;
using System;

public class PlayerController : TimeAffected
{
	// Use this for initialization
	public void Start()
	{
        GetComponent<LayeredController>().LayerChangedEventHandler += UpdateLayerTransparencyOnLayerChange;
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
			Layer++;
		}

        if (Input.GetKeyDown(KeyCode.S))
        {
            ShadowBlink();
        }
	}

    private void ShadowBlink()
	{
		transform.position = Shadow.transform.position;
        Layer = Layer.FindByZ(transform.position.z);
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
