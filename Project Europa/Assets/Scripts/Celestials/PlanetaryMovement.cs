using UnityEngine;

public class PlanetaryMovement : CelestialMovement
{
    private PlanetData planetData;
    private CelestialObjectData starData;
    private bool alreadyInOrbit;

    override protected void Awake()
    {
        base.Awake();
        starData = SolarSystemGenerator.Instance.Star;
        planetData = (PlanetData)celestialData;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlanetaryOrbit orbit = planetData.Orbit;
        switch (planetData.ObjectStaus)
        {
            case CelestialObjectData.CelestialObjectStaus.Drifting:
                planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
                if (WithinOrbitLimits(starData.transform.position, orbit.OuterRadius, orbit.InnerRadius))
                {
                    AddInitialOrbitVelocityToStar();
                    orbit.onOrbitEnter?.Invoke();
                    planetData.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
                }
                break;
            case CelestialObjectData.CelestialObjectStaus.Orbiting:
                planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
                if (!WithinOrbitLimits(starData.transform.position, orbit.OuterRadius, orbit.InnerRadius))
                {
                    orbit.onOrbitExit?.Invoke();
                    planetData.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                }
                break;
            case CelestialObjectData.CelestialObjectStaus.Tethered:
                if (WithinOrbitLimits(starData.transform.position, orbit.OuterRadius, orbit.InnerRadius))
                {
                    if (!alreadyInOrbit)
                    {
                        orbit.onOrbitEnter?.Invoke();
                        alreadyInOrbit = true;
                    }
                }
                else
                {
                    if (alreadyInOrbit)
                    {
                        orbit.onOrbitExit?.Invoke();
                        alreadyInOrbit = false;
                    }
                }
                break;
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

    private void AddPlanetInitialForce(CelestialObjectData _target)
    {
        int cw = -1;
        if (planetData.ClockwiseOrbit)
            cw *= cw;
        Vector3 initialVelocity = GetInitialVelocity(_target) * cw;
        planetData.RigidBody.velocity += initialVelocity;
        planetData.RigidBody.velocity = Vector3.ClampMagnitude(planetData.RigidBody.velocity, initialVelocity.magnitude);
    }

    private void AddInitialOrbitVelocityToStar()
    {
        AddPlanetInitialForce(SolarSystemGenerator.Instance.Star);
    }

    private bool WithinOrbitLimits(Vector3 _orbitCenter, float _outerLimit, float _innerLimit)
    {
        float sqrMag = Vector3.SqrMagnitude(planetData.transform.position - _orbitCenter);
        if (sqrMag < _outerLimit * _outerLimit && sqrMag > _innerLimit * _innerLimit)
            return true;
        return false;
    }
}
