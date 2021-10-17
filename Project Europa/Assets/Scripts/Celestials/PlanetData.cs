using UnityEngine;
using Sirenix.OdinInspector;
using Procedural;

public class PlanetData : CelestialObjectData
{
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary planetLibrary;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool clockwiseOrbit;

    //public delegate void OnStateStart(CelestialObjectStaus _objectStaus);
    //public OnStateStart stateStart;

    //public delegate void OnStateEnd(CelestialObjectStaus _objectStaus);
    //public OnStateEnd stateEnd;

    public delegate void OnOrbitEnter();
    public OnOrbitEnter onOrbitEnter;

    //public delegate void OnOrbitEnd();
    //public OnOrbitEnd onOrbitEnd;

    //public delegate void OnDriftStart();
    //public OnDriftStart onDriftStart;

    //public delegate void OnDriftEnd();
    //public OnDriftEnd onDriftEnd;

    //public delegate void OnTetherStart();
    //public OnTetherStart onTetherStart;

    //public delegate void OnTetherEnd();
    //public OnTetherEnd onTetherEnd;

    public delegate void OnDestroyed();
    public OnDestroyed onDestroyed;
    public ProceduralPlanetLibrary PlanetLibrary { get { return planetLibrary; } }
    public bool ClockwiseOrbit { get { return clockwiseOrbit; } }

    private void Awake()
    {
        InitializeProceduralData();
        SetComponentsValues();
        onDestroyed += DestroyPlanet;
    }

    private void OnDisable()
    {
        //stateStart = null;
        //stateEnd = null;
        onOrbitEnter = null;
        //onOrbitEnd = null;
        //onDriftStart = null;
        //onDriftEnd = null;
        //onTetherStart = null;
        //onTetherEnd = null;
        onDestroyed = null;
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
            //stateEnd?.Invoke(objectStaus);
            base.SetObjectStatus(_newStatus);
            //stateStart?.Invoke(objectStaus);
        } 
    }
    //public override void SetObjectStatus(CelestialObjectStaus _newStatus)
    //{
    //    if(_newStatus != objectStaus)
    //    {
    //        switch (objectStaus)
    //        {
    //            case CelestialObjectStaus.Orbiting:
    //                onOrbitEnd?.Invoke();
    //                break;
    //            case CelestialObjectStaus.Drifting:
    //                onDriftEnd?.Invoke();
    //                break;
    //            case CelestialObjectStaus.Tethered:
    //                onTetherEnd?.Invoke();
    //                break;
    //        }
    //        base.SetObjectStatus(_newStatus);
    //        switch (objectStaus)
    //        {
    //            case CelestialObjectStaus.Orbiting:
    //                onOrbitStart?.Invoke();
    //                break;
    //            case CelestialObjectStaus.Drifting:
    //                onDriftStart?.Invoke();
    //                break;
    //            case CelestialObjectStaus.Tethered:
    //                onTetherStart?.Invoke();
    //                break;
    //            case CelestialObjectStaus.Destroyed:
    //                onDestroyed?.Invoke();
    //                break;
    //        }
    //    }
    //}

    private void DestroyPlanet()
    {
        gameObject.SetActive(false);
    }
}
