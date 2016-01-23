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
		if (color == LayersEnum.Colors.red)
			transform.Translate(0, 0, (float)LayersEnum.Positions.first);
		else if (color == LayersEnum.Colors.blue)
			transform.Translate(0, 0, (float)LayersEnum.Positions.middle);
		else if (color == LayersEnum.Colors.green)
			transform.Translate(0, 0, (float)LayersEnum.Positions.last);
	}

	protected void step()
	{

	}
	
}
