using UnityEngine;
using System.Collections;

public class LayersEnum
{
	public enum Colors
	{
		red,
		blue,
		green
	}

	public enum Positions
	{
		first = 0,
		middle = 3,
		last = 6
	}

	public static Colors zToColor(float zz)
	{
		Positions z = (Positions)zz;
		if (z == Positions.first)
			return Colors.red;
		else if (z == Positions.middle)
			return Colors.blue;
		return Colors.green;
	}

	public static float colorToZ(Colors c)
	{
		if (c == Colors.red)
			return (float)Positions.first;
		else if (c == Colors.blue)
			return (float)Positions.middle;
		return (float)Positions.last;
	}
}
