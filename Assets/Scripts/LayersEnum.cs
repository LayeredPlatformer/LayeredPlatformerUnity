using UnityEngine;
using System.Collections;

public class LayersEnum
{
	public enum Colors
	{
		Red,
		Blue,
		Green
	}

	public enum Positions
	{
		First = 0,
		Middle = 3,
		Last = 6
	}

	public static Colors ZToColor(float zz)
	{
		Positions z = (Positions)zz;
		if (z == Positions.First)
			return Colors.Red;
		else if (z == Positions.Middle)
			return Colors.Blue;
		return Colors.Green;
	}

	public static float ColorToZ(Colors c)
	{
		if (c == Colors.Red)
			return (float)Positions.First;
		else if (c == Colors.Blue)
			return (float)Positions.Middle;
		return (float)Positions.Last;
	}
}
