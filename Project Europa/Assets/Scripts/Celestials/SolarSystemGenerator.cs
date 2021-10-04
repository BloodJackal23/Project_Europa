using UnityEngine;
using Sirenix.OdinInspector;

public class SolarSystemGenerator : Singleton<SolarSystemGenerator>
{
    [FoldoutGroup("Children"), SerializeField] private CelestialObject star;
    [FoldoutGroup("Children"), SerializeField] private CelestialObject[] planets;
    [FoldoutGroup("Children"), SerializeField] private PlanetaryOrbit[] orbits;

    [FoldoutGroup("Attributes"), SerializeField, Range(1, 7)] int activeOrbits;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 999999999f)] private float g;

    public CelestialObject Star { get { return star; } }
    public float G { get { return g; } }

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
}
