using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class PlanetaryMovement : CelestialMovement
{
    private PlanetData planetData;

    override protected void Awake()
    {
        base.Awake();
        planetData = (PlanetData)celestialData;
        planetData.stateStart += SetPlanetMovementState;
        //planetData.onOrbitStart += AddInitialOrbitVelocityToStar;
        //planetData.onTetherStart += TetherPlanet;
        //planetData.onTetherEnd += AddStarGravity;
    }

    private void AddPlanetInitialForce(CelestialObjectData _target)
    {
        int cw = -1;
        if (planetData.ClockwiseOrbit)
            cw *= cw;
        planetData.RigidBody.velocity += GetInitialVelocity(_target) * cw;
    }

    //public void StartPlanetaryOrbit()
    //{
    //    if (planetData.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Orbiting)
    //    {
    //        if(planetData.ObjectStaus == CelestialObjectData.CelestialObjectStaus.Tethered)
    //            AddStarGravity();
    //        planetData.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
    //        AddInitialOrbitVelocityToStar();
    //    }
    //}

    private void SetPlanetMovementState(CelestialObjectData.CelestialObjectStaus _staus)
    {
        StopAllCoroutines();
        switch (_staus)
        {
            case CelestialObjectData.CelestialObjectStaus.Drifting:
                AddStarGravity();
                break;
            case CelestialObjectData.CelestialObjectStaus.Orbiting:
                AddInitialOrbitVelocityToStar();
                AddStarGravity();
                break;
            case CelestialObjectData.CelestialObjectStaus.Destroyed:
                planetData.RigidBody.velocity *= 0;
                planetData.DestroyPlanet();
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

    //private IEnumerator StartOrbitalMovement()
    //{
    //    planetData.RigidBody.velocity *= 0;
    //    CelestialObjectData starData = SolarSystemGenerator.Instance.Star;
    //    AddPlanetInitialForce(starData);
    //    while (planetData.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered || planetData.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Destroyed)
    //    {
    //        planetData.RigidBody.AddForce((starData.transform.position - transform.position).normalized * GetGravitationalForce(starData));
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    private IEnumerator StartGravitationalForce(CelestialObjectData _targetObject)
    {
        while (planetData.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered || planetData.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Destroyed)
        {
            planetData.RigidBody.AddForce((_targetObject.transform.position - transform.position).normalized * GetGravitationalForce(_targetObject));
            yield return new WaitForFixedUpdate();
        }
    }

    private void AddStarGravity()
    {
        StartCoroutine(StartGravitationalForce(SolarSystemGenerator.Instance.Star));
    }

    private void AddInitialOrbitVelocityToStar()
    {
        AddPlanetInitialForce(SolarSystemGenerator.Instance.Star);
    }

    //public void SetTethered(bool _tethered)
    //{
    //    IsTethered = _tethered;
    //}
}
