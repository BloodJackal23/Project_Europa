using UnityEngine;
using Sirenix.OdinInspector;
using Procedural;

public class PlanetData : CelestialObjectData
{
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary library;
    [FoldoutGroup("Attributes"), SerializeField] private CelestialAttributes attributes;

    public ProceduralPlanetLibrary Library { get { return library; } }
    public CelestialAttributes Attributes { get { return attributes; } }
    public bool ClockwiseOrbit { get; private set; }

    private void Awake()
    {
        InitializeProceduralData();
        SetComponentsValues();
    }

    private void InitializeProceduralData()
    {
        attributes = CelestialProceduralGenerator.RandomizedAttributes(library);
        ClockwiseOrbit = true;
        if (Random.Range(0, 2) > 0)
            ClockwiseOrbit = false;
    }

    private void SetComponentsValues()
    {
        rigidbody.mass = attributes.Mass;
        transform.localScale *= attributes.Scale;
        //if(collider.GetType() == typeof(SphereCollider))
        //{
        //    SphereCollider sphereCollider = collider.GetComponent<SphereCollider>();
        //    sphereCollider.radius *= attributes.Scale;
        //}
        meshRenderer.material = attributes.ObjectMaterial;
    }
}
