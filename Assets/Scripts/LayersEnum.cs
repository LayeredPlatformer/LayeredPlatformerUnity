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

	public static Colors ZToColor(float z)
	{
		var layerPosition = (Positions)z;
        if (layerPosition == Positions.First)
        {
            return Colors.Red;
        }
        else if (layerPosition == Positions.Middle)
        {
            return Colors.Blue;
        }
		return Colors.Green;
	}

	public static float ColorToZ(Colors color)
	{
        if (color == Colors.Red)
        {
            return (float)Positions.First;
        }
        else if (color == Colors.Blue)
        {
            return (float)Positions.Middle;
        }
		return (float)Positions.Last;
	}
}
