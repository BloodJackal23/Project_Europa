using UnityEngine;
using Sirenix.OdinInspector;
using Procedural;

public class PlanetData : CelestialObjectData
{
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary planetLibrary;
    [FoldoutGroup("Components"), SerializeField] private PlanetMarker marker;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private PlanetaryOrbit orbit;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool clockwiseOrbit;

    public delegate void OnDestroyed();
    public OnDestroyed onDestroyed;
    public ProceduralPlanetLibrary PlanetLibrary { get { return planetLibrary; } }
    public PlanetaryOrbit Orbit { get { return orbit; } }
    public bool ClockwiseOrbit { get { return clockwiseOrbit; } }

    override protected void Awake()
    {
        base.Awake();
        InitializeProceduralData();
        SetComponentsValues();
        onDestroyed += DestroyPlanet;
    }

    private void OnEnable()
    {
        marker.transform.parent = null;
        marker.transform.localScale = new Vector3(marker.Scale, marker.Scale, 1);
        marker.transform.parent = transform;
        marker.InitializeMarker(transform);
        orbit.onOrbitEnter += marker.SetMarkerToOnOrbit;
        orbit.onOrbitExit += marker.SetMarkerToOffOrbit;
    }

    private void OnDisable()
    {
        onDestroyed = null;
        orbit.onOrbitEnter = null;
        orbit.onOrbitExit = null;
    }

    private void InitializeProceduralData()
    {
        attributes = CelestialProceduralGenerator.RandomizedAttributes(planetLibrary);
        clockwiseOrbit = true;
        if (Random.Range(0, 2) > 0)
            clockwiseOrbit = false;
    }

    private void SetComponentsValues()
    {
        rigidbody.mass = attributes.Mass;
        transform.localScale *= attributes.Scale;
        meshRenderer.material = attributes.ObjectMaterial;
    }
    public override void SetObjectStatus(CelestialObjectStaus _newStatus)
    {
        if(objectStaus != _newStatus)
        {
            base.SetObjectStatus(_newStatus);
        } 
    }

    public void SetOrbit(PlanetaryOrbit _orbit)
    {
        orbit = _orbit;
    }

    private void DestroyPlanet()
    {
        gameObject.SetActive(false);
    }
}
