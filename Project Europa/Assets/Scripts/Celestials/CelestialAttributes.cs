using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CelestialAttributes
{
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private float mass;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private float scale;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private string name;
    [FoldoutGroup("Procedural Attributes"), SerializeField, ReadOnly] private Material objectMaterial;
    public float Mass { get { return mass; } }
    public float Scale { get { return scale; } }
    public string Name { get { return name; } }
    public Material ObjectMaterial { get { return objectMaterial; } }

    public CelestialAttributes(float _mass, float _scale, string _name, Material _mat)
    {
        mass = _mass;
        scale = _scale;
        name = _name;
        objectMaterial = _mat;
    }
}
