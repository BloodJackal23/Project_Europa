using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObjectData : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected Rigidbody rigidbody;
    [FoldoutGroup("Components"), SerializeField] protected MeshRenderer meshRenderer;

    [FoldoutGroup("Attributes"), SerializeField] protected CelestialAttributes attributes;
    [FoldoutGroup("Attributes"), SerializeField] protected CelestialObjectStaus initialStatus = CelestialObjectStaus.Stationary;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] protected CelestialObjectStaus objectStaus;

    public enum CelestialObjectStaus {Stationary, Orbiting, Drifting, Tethered, Destroyed}
    public CelestialObjectStaus ObjectStaus { get { return objectStaus; } }
    public Rigidbody RigidBody { get 
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            return rigidbody; 
        } 
    }
    public CelestialAttributes Attributes { get { return attributes; } }

    virtual protected void Awake()
    {
        objectStaus = initialStatus;
    }

    public virtual void SetObjectStatus(CelestialObjectStaus _newStatus)
    {
        objectStaus = _newStatus;
    }
}