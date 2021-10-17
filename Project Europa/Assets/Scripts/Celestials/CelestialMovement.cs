using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CelestialObjectData))]
public class CelestialMovement : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected CelestialObjectData celestialData;

    virtual protected void Awake()
    {
        if (celestialData == null)
            celestialData = GetComponent<CelestialObjectData>();
    }

    virtual protected void FixedUpdate()
    {

    }

    public float GetGravitationalForce(CelestialObjectData _target)
    {
        if (_target == this)
            return 0;
        float m1 = celestialData.RigidBody.mass;
        float m2 = _target.RigidBody.mass;
        float r = (_target.transform.position - transform.position).sqrMagnitude;
        return SolarSystemGenerator.Instance.G * (m1 * m2) / r;
    }
}
