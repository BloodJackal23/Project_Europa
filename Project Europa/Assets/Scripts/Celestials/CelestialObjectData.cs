using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObjectData : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected Rigidbody rigidbody;
    [FoldoutGroup("Components"), SerializeField] protected Collider collider;
    [FoldoutGroup("Components"), SerializeField] protected MeshRenderer meshRenderer;
    public Rigidbody RigidBody { get { return rigidbody; } }
    public string ObjectName { get; private set; }

    public float GetGravitationalForce(CelestialObjectData _target)
    {
        if (_target == this)
            return 0;
        float m1 = rigidbody.mass;
        float m2 = _target.rigidbody.mass;
        float r = Vector3.Magnitude(_target.transform.position - transform.position);
        return (SolarSystemGenerator.instance.G * (m1 * m2) / (r * r));
    }

    public Vector3 GetInitialVelocity(CelestialObjectData _target)
    {
        if (_target == this)
            return Vector3.zero;
        float m2 = _target.rigidbody.mass;
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Vector3 perp = new Vector3(-direction.z, 0, direction.x);
        float r = Vector3.Distance(transform.position, _target.transform.position);
        return perp * Mathf.Sqrt((SolarSystemGenerator.instance.G * m2) / r);
    }

    public void SetName(string _value)
    {
        ObjectName = _value;
    }
}