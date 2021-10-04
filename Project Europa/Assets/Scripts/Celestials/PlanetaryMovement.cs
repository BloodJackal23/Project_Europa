using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CelestialObject))]
public class PlanetaryMovement : CelestialMovement
{
    [FoldoutGroup("Attributes"), SerializeField] protected bool clockwiseOrbit = true;
    public bool IsOrbiting { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        celestialObject.RigidBody.mass = Random.Range(1f, 100f);
        StartPlanetaryOrbit();
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
        IsOrbiting = true;
        StartCoroutine(RunOrbit());
    }

    public void StopPlanetaryOrbit()
    {
        StopAllCoroutines();
        IsOrbiting = false;
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
}
