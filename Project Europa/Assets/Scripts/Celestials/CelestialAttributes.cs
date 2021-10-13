using UnityEngine;

public class CelestialAttributes
{
    public float Mass { get; private set; }
    public float Scale { get; private set; }
    public string Name { get; private set; }
    public Material ObjectMaterial { get; private set; }

    public CelestialAttributes(float _mass, float _scale, string _name, Material _mat)
    {
        Mass = _mass;
        Scale = _scale;
        Name = _name;
        ObjectMaterial = _mat;
    }
}
