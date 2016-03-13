using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Layer
{
    public enum Labels { Front, Middle, Back }

    public int Index { get; private set; }

    public float Z { get; private set; }

    public Labels Label { get; private set; }

    public static Layer[] AllLayers =
    {
        new Layer(index: 0, z: 0, label: Labels.Front),
        new Layer(index: 1, z: 1.5f, label: Labels.Middle),
        new Layer(index: 2, z: 3, label: Labels.Back)
    };

    public static Layer FindByZ(float z)
    {
        return AllLayers.Single(x => x.Z == z);
    }

    public static Layer FindByLabel(Labels label)
    {
        return AllLayers.Single(x => x.Label == label);
    }

    public static Layer operator ++(Layer old)
    {
        return AllLayers[(old.Index + 1) % AllLayers.Length];
    }
    public static Layer operator --(Layer old)
    {
        return AllLayers[(old.Index - 1 + AllLayers.Length) % AllLayers.Length];
    }

    private Layer(int index, float z, Labels label)
	{
        Index = index;
        Z = z;
        Label = label;
    }

	public bool Equals(Layer l)
	{
		return l.Z == Z;
	}
}
