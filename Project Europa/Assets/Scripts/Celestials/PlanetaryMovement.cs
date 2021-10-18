using UnityEngine;

public class PlanetaryMovement : CelestialMovement
{
    private PlanetData planetData;
    private bool inOrbit;

    override protected void Awake()
    {
        base.Awake();
        planetData = (PlanetData)celestialData;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CelestialObjectData starData = SolarSystemGenerator.Instance.Star;
        PlanetaryOrbit orbit = planetData.Orbit;
        float sqrMag = Vector3.SqrMagnitude(planetData.transform.position - starData.transform.position);

        switch (planetData.ObjectStaus)
        {
            case CelestialObjectData.CelestialObjectStaus.Drifting:
                planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
                if (sqrMag < orbit.OuterRadius * orbit.OuterRadius && sqrMag > orbit.InnerRadius * orbit.InnerRadius)
                {
                    Debug.Log("Planet: " + planetData.gameObject.name + " is in orbit!");
                    AddInitialOrbitVelocityToStar();
                    orbit.onOrbitEnter?.Invoke();
                    planetData.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
                }
                break;
            case CelestialObjectData.CelestialObjectStaus.Orbiting:
                planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
                if (sqrMag > orbit.OuterRadius * orbit.OuterRadius || sqrMag < orbit.InnerRadius * orbit.InnerRadius)
                {
                    Debug.Log("Planet: " + planetData.gameObject.name + " is out of orbit!");
                    orbit.onOrbitExit?.Invoke();
                    planetData.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                }
                break;
            case CelestialObjectData.CelestialObjectStaus.Tethered:
                if (sqrMag < orbit.OuterRadius * orbit.OuterRadius && sqrMag > orbit.InnerRadius * orbit.InnerRadius)
                {
                    if (!inOrbit)
                    {
                        orbit.onOrbitEnter?.Invoke();
                        inOrbit = true;
                    }
                }
                else
                {
                    if (inOrbit)
                    {
                        orbit.onOrbitExit?.Invoke();
                        inOrbit = false;
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
}
