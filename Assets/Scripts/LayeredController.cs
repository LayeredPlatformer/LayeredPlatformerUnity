using UnityEngine;
using System.Collections;
using System;

public class LayeredController : MonoBehaviour
{
    public event EventHandler LayerChanged;

    [SerializeField]
	private LayersEnum.Colors _layer;

    public LayersEnum.Colors Layer
    {
        get
        {
            return _layer;
        }
        set
        {
            _layer = value;
            OnLayerChanged();
        }
    }

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
        OnLayerChanged();
	}

	protected virtual void Step()
	{

	}

    protected virtual void OnLayerChanged()
    {
        if (LayerChanged != null)
        {
            LayerChanged(this, new LayerChangedEventArgs { NewLayer = Layer });
        }
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

    public class LayerChangedEventArgs : EventArgs
    {
        public LayersEnum.Colors NewLayer { get; set; }
    }
}
