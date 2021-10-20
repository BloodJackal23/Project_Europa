using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CelestialAttributes
{
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private float density;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private float volumeMultiplier;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private string name;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private Material objectMaterial;

    public float Density { get { return density; } }
    public float VolumeMultiplier { get { return volumeMultiplier; } }
    public string Name { get { return name; } }
    public Material ObjectMaterial { get { return objectMaterial; } }

    public CelestialAttributes(float _density, float _volume, string _name, Material _mat)
    {
        density = _density;
        volumeMultiplier = _volume;
        name = _name;
        objectMaterial = _mat;
    }
}
