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

        if (_layer == LayersEnum.Colors.Red)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.First);
        }
        else if (_layer == LayersEnum.Colors.Blue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.Middle);
        }
        else if (_layer == LayersEnum.Colors.Green)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.Last);
        }
	}

	protected void CycleLayers()
	{
        if (_layer == LayersEnum.Colors.Red)
        {
            UpdateLayer(LayersEnum.Colors.Blue);
        }
        else if (_layer == LayersEnum.Colors.Blue)
        {
            UpdateLayer(LayersEnum.Colors.Green);
        }
        else if (_layer == LayersEnum.Colors.Green)
        {
            UpdateLayer(LayersEnum.Colors.Red);
        }
	}

    private class LayerChangedEventArgs : EventArgs
    {
        public LayersEnum.Colors NewLayer { get; set; }
    }
}
