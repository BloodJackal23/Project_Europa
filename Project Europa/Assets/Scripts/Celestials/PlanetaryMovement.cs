using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CelestialObject))]
public class PlanetaryMovement : CelestialMovement
{
    public delegate void OnDetached();
    public OnDetached onDetached;
    [FoldoutGroup("Attributes"), SerializeField] protected bool clockwiseOrbit = true;
    public bool IsOrbiting { get; private set; }
    public bool IsTethered { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        celestialObject.RigidBody.mass = Random.Range(1f, 100f);
    }

    private void AddPlanetForwardForce()
    {
        int cw = -1;
        if (clockwiseOrbit)
            cw *= cw;
        celestialObject.RigidBody.velocity += celestialObject.GetInitialVelocity(SolarSystemGenerator.instance.Star) * cw;
    }

    public void SetOrbitDirection(bool _clockwise)
    {
        clockwiseOrbit = _clockwise;
    }

    public void StartPlanetaryOrbit()
    {
        if (!IsOrbiting)
        {
            IsOrbiting = true;
            StartCoroutine(RunOrbit());
        }
    }

    public void StopPlanetaryOrbit()
    {
        if (IsOrbiting)
        {
            StopAllCoroutines();
            IsOrbiting = false;
        }
    }

    private IEnumerator RunOrbit()
    {
        celestialObject.RigidBody.velocity *= 0;
        AddPlanetForwardForce();
        while (IsOrbiting)
        {
            celestialObject.RigidBody.AddForce((SolarSystemGenerator.instance.Star.transform.position - transform.position).normalized
                * celestialObject.GetGravitationalForce(SolarSystemGenerator.instance.Star));
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetTethered(bool _tethered)
    {
        IsTethered = _tethered;
    }
}
