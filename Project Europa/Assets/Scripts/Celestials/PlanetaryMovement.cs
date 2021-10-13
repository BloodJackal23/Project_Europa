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

    private void Awake()
    {
        if (planetData == null)
            planetData = GetComponent<PlanetData>();
    }

    private void AddPlanetForwardForce()
    {
        int cw = -1;
        if (planetData.ClockwiseOrbit)
            cw *= cw;
        planetData.RigidBody.velocity += GetInitialOrbitalVelocity(SolarSystemGenerator.instance.Star) * cw;
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

    public Vector3 GetInitialOrbitalVelocity(CelestialObjectData _target)
    {
        if (_target == this)
            return Vector3.zero;
        float m2 = _target.RigidBody.mass;
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Vector3 perp = new Vector3(-direction.z, 0, direction.x);
        float r = Vector3.Distance(transform.position, _target.transform.position);
        return perp * Mathf.Sqrt((SolarSystemGenerator.instance.G * m2) / r);
    }

    private IEnumerator RunOrbit()
    {
        planetData.RigidBody.velocity *= 0;
        AddPlanetForwardForce();
        while (IsOrbiting)
        {
            planetData.RigidBody.AddForce((SolarSystemGenerator.instance.Star.transform.position - transform.position).normalized
                * GetGravitationalForce(SolarSystemGenerator.instance.Star));
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetTethered(bool _tethered)
    {
        IsTethered = _tethered;
    }
}
