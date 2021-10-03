using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObject : MonoBehaviour
{    
    public const float G = 1000000f;
    public Rigidbody RigidBody { get; private set; }
    public string ObjectName { get; private set; }

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    public float GetGravitationalForce(CelestialObject _target)
    {
        if (_target == this)
            return 0;
        float m1 = RigidBody.mass;
        float m2 = _target.RigidBody.mass;
        float r = Vector3.Magnitude(_target.transform.position - transform.position);
        return (G * (m1 * m2) / (r * r));
    }

    public Vector3 GetInitialVelocity(CelestialObject _target)
    {
        if (_target == this)
            return Vector3.zero;
        float m2 = _target.RigidBody.mass;
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Vector3 perp = new Vector3(-direction.z, 0, direction.x);
        float r = Vector3.Distance(transform.position, _target.transform.position);
        return perp * Mathf.Sqrt((G * m2) / r);
    }

    public void SetName(string _value)
    {
        ObjectName = _value;
    }
}
