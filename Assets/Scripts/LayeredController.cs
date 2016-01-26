using UnityEngine;
using System.Collections;
using System;

public class LayeredController : MonoBehaviour
{
    public event EventHandler LayerChanged;

#pragma warning disable 0649
    [SerializeField]
    private Layer.Labels _layerLabel;
#pragma warning restore 0649

    private Layer _layer;

    public Layer Layer
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
        Layer = Layer.FindByLabel(_layerLabel);
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
        transform.position = new Vector3(transform.position.x, transform.position.y, newLayer.Z);
    }

    public class LayerChangedEventArgs : EventArgs
    {
        public Layer NewLayer { get; set; }
    }
}
