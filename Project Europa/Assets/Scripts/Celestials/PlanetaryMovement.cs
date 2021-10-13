using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class PlanetaryMovement : CelestialMovement
{
    public delegate void OnDetached();
    public OnDetached onDetached;

    private PlanetData planetData;
    public bool IsOrbiting { get; private set; }
    public bool IsTethered { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        planetData = (PlanetData)celestialData;
    }

    private void AddPlanetInitialForce(CelestialObjectData _target)
    {
        int cw = -1;
        if (planetData.ClockwiseOrbit)
            cw *= cw;
        planetData.RigidBody.velocity += GetInitialVelocity(_target) * cw;
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

    private Vector3 GetInitialVelocity(CelestialObjectData _target)
    {
        if (_target == this)
            return Vector3.zero;
        float m2 = _target.RigidBody.mass;
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Vector3 perp = new Vector3(-direction.z, 0, direction.x);
        float r = Vector3.Distance(transform.position, _target.transform.position);
        return perp * Mathf.Sqrt(SolarSystemGenerator.Instance.G * m2 / r);
    }

    private IEnumerator RunOrbit()
    {
        planetData.RigidBody.velocity *= 0;
        CelestialObjectData starData = SolarSystemGenerator.Instance.Star;
        AddPlanetInitialForce(starData);
        while (!IsTethered)
        {
            planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetTethered(bool _tethered)
    {
        IsTethered = _tethered;
    }
}
