using UnityEngine;
using System.Collections;

public class ColoredController : MonoBehaviour
{
	public LayersEnum.Colors color;

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
		Debug.Log("color: " + color);
		if (color == LayersEnum.Colors.red)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.first);
		else if (color == LayersEnum.Colors.blue)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.middle);
		else if (color == LayersEnum.Colors.green)
			transform.position = new Vector3(transform.position.x, transform.position.y, (float)LayersEnum.Positions.last);
	}

	protected void step()
	{

	}
	
}
