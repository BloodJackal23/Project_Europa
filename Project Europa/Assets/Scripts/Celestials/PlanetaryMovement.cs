using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[RequireComponent(typeof(PlanetData))]
public class PlanetaryMovement : CelestialMovement
{
    public delegate void OnDetached();
    public OnDetached onDetached;

    [FoldoutGroup("Components"), SerializeField] private PlanetData planetData;
    public bool IsOrbiting { get; private set; }
    public bool IsTethered { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (planetData == null)
            planetData = GetComponent<PlanetData>();
    }

    private void AddPlanetForwardForce()
    {
        int cw = -1;
        if (planetData.ClockwiseOrbit)
            cw *= cw;
        planetData.RigidBody.velocity += planetData.GetInitialVelocity(SolarSystemGenerator.instance.Star) * cw;
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
        planetData.RigidBody.velocity *= 0;
        AddPlanetForwardForce();
        while (IsOrbiting)
        {
            planetData.RigidBody.AddForce((SolarSystemGenerator.instance.Star.transform.position - transform.position).normalized
                * planetData.GetGravitationalForce(SolarSystemGenerator.instance.Star));
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetTethered(bool _tethered)
    {
        IsTethered = _tethered;
    }
}
