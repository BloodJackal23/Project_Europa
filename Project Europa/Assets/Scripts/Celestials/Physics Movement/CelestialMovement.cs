using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CelestialObjectData))]
public class CelestialMovement : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected CelestialObjectData celestialData;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 500f)] protected float gravitationalRadius = 150f;
    [FoldoutGroup("Attributes"), SerializeField] protected LayerMask gravitationalTargetMask;

    virtual protected void Awake()
    {
        if (celestialData == null)
            celestialData = GetComponent<CelestialObjectData>();
    }

    protected float GetGravitationalForce(CelestialObjectData _target)
    {
        if (_target == this)
            return 0;
        float m1 = celestialData.RigidBody.mass;
        float m2 = _target.RigidBody.mass;
        float r = (_target.transform.position - transform.position).sqrMagnitude;
        return SolarSystem.Instance.G * (m1 * m2) / r;
    }

    protected void AddGravitationalForce(CelestialObjectData _target, float _forceModifier = 1)
    {
        celestialData.RigidBody.AddForce((_target.transform.position - transform.position).normalized * GetGravitationalForce(_target) * _forceModifier);
    }

    protected void AddGravitationalForceFromOtherCelestialBodies(float _gravitationMultiplier = 1)
    {
        Collider[] otherBodies = Physics.OverlapSphere(transform.position, gravitationalRadius, gravitationalTargetMask, QueryTriggerInteraction.Ignore);
        foreach(Collider body in otherBodies)
        {
            if(body != GetComponent<Collider>() && body.TryGetComponent(out PlanetData planet))
            {
                if(planet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
                    AddGravitationalForce(planet, _gravitationMultiplier);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gravitationalRadius);
    }
}
