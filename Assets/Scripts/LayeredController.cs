using UnityEngine;
using System.Collections;

public class LayeredController : MonoBehaviour
{
	public LayersEnum.Colors _layer;

	// Use this for initialization
	void Start ()
	{
		initialize();
	}

	// Update is called once per frame
	void Update ()
	{
		step();
	}

	protected void initialize()
	{
		updateLayer(_layer);
	}

	protected void step()
	{

	}

	protected void updateLayer(LayersEnum.Colors l)
	{
		_layer = l;
		if (_layer == LayersEnum.Colors.red)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.first);
		else if (_layer == LayersEnum.Colors.blue)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.middle);
		else if (_layer == LayersEnum.Colors.green)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.last);
	}

	protected void cycleLayers()
	{
		if (_layer == LayersEnum.Colors.red)
			updateLayer(LayersEnum.Colors.blue);
		else if (_layer == LayersEnum.Colors.blue)
			updateLayer(LayersEnum.Colors.green);
		else if (_layer == LayersEnum.Colors.green)
			updateLayer(LayersEnum.Colors.red);
	}

}
