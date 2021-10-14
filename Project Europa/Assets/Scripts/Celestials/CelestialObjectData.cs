using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObjectData : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected Rigidbody rigidbody;
    [FoldoutGroup("Components"), SerializeField] protected MeshRenderer meshRenderer;

    [FoldoutGroup("Attributes"), SerializeField] protected CelestialAttributes attributes;

    public bool IsDestroyed = false;

    public Rigidbody RigidBody { get 
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            return rigidbody; 
        } 
    }
    public CelestialAttributes Attributes { get { return attributes; } }

    public void DestroyBody(bool _disableGameObject = true)
    {
        IsDestroyed = true;
        if (_disableGameObject)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}