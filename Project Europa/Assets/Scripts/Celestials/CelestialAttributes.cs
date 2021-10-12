using UnityEngine;

public class CelestialAttributes
{
    public float Mass { get; private set; }
    public float Scale { get; private set; }
    public float OrbitSpeed { get; private set; }
    public string Name { get; private set; }
    public Material ObjectMaterial { get; private set; }

    public CelestialAttributes(float _mass, float _scale, float _speed, string _name, Material _mat)
    {
        Mass = _mass;
        Scale = _scale;
        OrbitSpeed = _speed;
        Name = _name;
        ObjectMaterial = _mat;
    }
}
