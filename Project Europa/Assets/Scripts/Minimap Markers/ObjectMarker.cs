using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectMarker : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected SpriteRenderer spriteRenderer;
    [FoldoutGroup("Attributes"), SerializeField] protected Sprite mainSprite;
    [FoldoutGroup("Attributes"), SerializeField] protected float yOffset = 100f;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 500f)] protected float scale = 300f;

    public Sprite MainSprite { get { return mainSprite; } }
    public float YOffset { get { return yOffset; } }
    public float Scale { get { return scale; } }

    public void InitializeMarker(Transform _target)
    {
        transform.position = new Vector3(_target.position.x, yOffset, _target.position.z);
    }
}
