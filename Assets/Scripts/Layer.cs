using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Layer
{
    public enum Labels { Red, Blue, Green }

    public int Index { get; set; }

    public float Z { get; set; }

    public Labels Label { get; set; }

    public static Layer[] AllLayers =
    {
        new Layer { Index = 0, Z = 0, Label = Labels.Red },
        new Layer { Index = 1, Z = 3, Label = Labels.Blue },
        new Layer { Index = 2, Z = 6, Label = Labels.Green }
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
}
