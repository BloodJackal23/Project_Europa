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
        InitializeProceduralData();
        SetComponentsValues();
        onDestroyed += DestroyPlanet;
    }

    override protected void OnEnable()
    {
        base.OnEnable();
        planetMarker = (PlanetMarker)objectMarker;
        planetMarker.transform.parent = null;
        planetMarker.transform.localScale = new Vector3(planetMarker.Scale, planetMarker.Scale, 1);
        planetMarker.transform.parent = transform;
        orbit.onOrbitEnter += planetMarker.SetMarkerToOnOrbit;
        orbit.onOrbitExit += planetMarker.SetMarkerToOffOrbit;
        onDestroyed += ScoreManager.Instance.ReduceRemainingPlanetsByOne;
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
        rigidbody.mass = attributes.Density * attributes.VolumeMultiplier;
        transform.localScale *= attributes.VolumeMultiplier;
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
