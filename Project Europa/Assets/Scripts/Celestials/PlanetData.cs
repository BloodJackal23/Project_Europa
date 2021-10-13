using UnityEngine;
using Sirenix.OdinInspector;
using Procedural;

public class PlanetData : CelestialObjectData
{
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary planetLibrary;

    public ProceduralPlanetLibrary PlanetLibrary { get { return planetLibrary; } }
    public bool ClockwiseOrbit { get; private set; }

    private void Awake()
    {
        InitializeProceduralData();
        SetComponentsValues();
    }

    private void InitializeProceduralData()
    {
        attributes = CelestialProceduralGenerator.RandomizedAttributes(planetLibrary);
        ClockwiseOrbit = true;
        if (Random.Range(0, 2) > 0)
            ClockwiseOrbit = false;
    }

    private void SetComponentsValues()
    {
        rigidbody.mass = attributes.Mass;
        transform.localScale *= attributes.Scale;
        meshRenderer.material = attributes.ObjectMaterial;
    }
}
