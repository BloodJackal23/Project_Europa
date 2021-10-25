using UnityEngine;
using Sirenix.OdinInspector;
using Procedural;

public class PlanetData : CelestialObjectData
{
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary planetLibrary;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private PlanetaryOrbit orbit;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool clockwiseOrbit;

    private PlanetMarker planetMarker;

    public delegate void OnDestroyed();
    public OnDestroyed onDestroyed;
    public ProceduralPlanetLibrary PlanetLibrary { get { return planetLibrary; } }
    public PlanetaryOrbit Orbit { get { return orbit; } }
    public bool ClockwiseOrbit { get { return clockwiseOrbit; } }

    override protected void Awake()
    {
        base.Awake();
    }

    override protected void OnEnable()
    {
        rigidbody.velocity *= 0;
        InitializeProceduralData();
        SetComponentsValues();
        onDestroyed += DestroyPlanet;
        planetMarker = (PlanetMarker)objectMarker;
        planetMarker.InitializeMarker(transform);
        orbit.onOrbitEnter += planetMarker.SetMarkerToOnOrbit;
        orbit.onOrbitExit += planetMarker.SetMarkerToOffOrbit;
        onDestroyed += LevelManager.Instance.ReduceRemainingPlanetsByOne;
    }

    private void OnDisable()
    {
        onDestroyed = null;
        orbit.onOrbitEnter -= planetMarker.SetMarkerToOnOrbit;
        orbit.onOrbitExit -= planetMarker.SetMarkerToOffOrbit;
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
        rigidbody.mass = attributes.Density * attributes.VolumeMultiplier;
        transform.localScale = new Vector3(attributes.VolumeMultiplier, attributes.VolumeMultiplier, attributes.VolumeMultiplier);
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
