using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObjectData : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected Rigidbody rigidbody;
    [FoldoutGroup("Components"), SerializeField] protected MeshRenderer meshRenderer;
    public Rigidbody RigidBody { get { return rigidbody; } }
    public string ObjectName { get; private set; }

    public void SetName(string _value)
    {
        ObjectName = _value;
    }
}