using UnityEngine;
using System.Collections;
using System;

public class LayeredController : MonoBehaviour
{
    public event EventHandler LayerChanged;

	public LayersEnum.Colors _layer;

	// Use this for initialization
	void Start()
	{
		Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		Step();
	}

	protected virtual void Initialize()
	{
		UpdateLayer(_layer);
	}

	protected virtual void Step()
	{

	}

    protected virtual void OnLayerChanged(EventArgs args)
    {
        if (LayerChanged != null)
        {
            LayerChanged(this, args);
        }
    }

	protected void UpdateLayer(LayersEnum.Colors newLayer)
	{
		_layer = newLayer;
        OnLayerChanged(new LayerChangedEventArgs { NewLayer = newLayer });

		if (_layer == LayersEnum.Colors.red)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.first);
		else if (_layer == LayersEnum.Colors.blue)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.middle);
		else if (_layer == LayersEnum.Colors.green)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.last);
	}

	protected void CycleLayers()
	{
		if (_layer == LayersEnum.Colors.red)
			UpdateLayer(LayersEnum.Colors.blue);
		else if (_layer == LayersEnum.Colors.blue)
			UpdateLayer(LayersEnum.Colors.green);
		else if (_layer == LayersEnum.Colors.green)
			UpdateLayer(LayersEnum.Colors.red);
	}

    private class LayerChangedEventArgs : EventArgs
    {
        public LayersEnum.Colors NewLayer { get; set; }
    }
}
