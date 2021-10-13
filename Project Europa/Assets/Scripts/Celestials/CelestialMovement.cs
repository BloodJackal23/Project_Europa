using UnityEngine;

public class CelestialMovement : MonoBehaviour
{
    public float GetGravitationalForce(CelestialObjectData _target)
    {
        if (_target == this)
            return 0;
        float m1 = GetComponent<Rigidbody>().mass;
        float m2 = _target.RigidBody.mass;
        float r = Vector3.Magnitude(_target.transform.position - transform.position);
        return SolarSystemGenerator.instance.G * (m1 * m2) / (r * r);
    }
}
