using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetaryMovement : CelestialMovement
{
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private float innerRadius;
    private PlanetData planetData;

    override protected void Awake()
    {
        base.Awake();
        planetData = (PlanetData)celestialData;
        planetData.onOrbitEnter += AddInitialOrbitVelocityToStar;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(planetData.ObjectStaus == CelestialObjectData.CelestialObjectStaus.Drifting || planetData.ObjectStaus == CelestialObjectData.CelestialObjectStaus.Orbiting)
        {
            CelestialObjectData starData = SolarSystemGenerator.Instance.Star;
            planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
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
        planetData.RigidBody.velocity += GetInitialVelocity(_target) * cw;
    }

    private void AddInitialOrbitVelocityToStar()
    {
        AddPlanetInitialForce(SolarSystemGenerator.Instance.Star);
    }
}
