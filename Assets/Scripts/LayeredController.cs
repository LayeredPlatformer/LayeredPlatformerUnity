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
        LayerChanged += UpdatePositionOnLayerChange;
        UpdateLayer(_layer);
	}

	protected virtual void Step()
	{

	}

    protected virtual void OnLayerChanged()
    {
        if (LayerChanged != null)
        {
            LayerChanged(this, new LayerChangedEventArgs { NewLayer = _layer });
        }
    }

	protected void UpdateLayer(LayersEnum.Colors newLayer)
	{
		_layer = newLayer;
        OnLayerChanged();
	}

    protected void UpdatePositionOnLayerChange(object sender, EventArgs args)
    {
        var newLayer = ((LayerChangedEventArgs)args).NewLayer;

        if (newLayer == LayersEnum.Colors.Red)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.First);
        }
        else if (newLayer == LayersEnum.Colors.Blue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.Middle);
        }
        else if (newLayer == LayersEnum.Colors.Green)
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

    public class LayerChangedEventArgs : EventArgs
    {
        public LayersEnum.Colors NewLayer { get; set; }
    }
}
