using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectMarker : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected SpriteRenderer spriteRenderer;
    [FoldoutGroup("Attributes"), SerializeField] protected Sprite mainSprite;
    [FoldoutGroup("Attributes"), SerializeField] protected float yOffset = 100f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] protected Transform targetObject;

    public Sprite MainSprite { get { return mainSprite; } }
    public float YOffset { get { return yOffset; } }
    public Transform TargetObject { get { return targetObject; } }

    virtual public void InitializeMarker(Transform _target)
    {
        targetObject = _target;
        transform.position = new Vector3(_target.position.x, yOffset, _target.position.z);
    }
}
