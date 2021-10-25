using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetMarker : ObjectMarker
{
    [FoldoutGroup("Attributes"), SerializeField] private Sprite offOrbitSprite;
    [FoldoutGroup("Attributes"), SerializeField] private Color onOrbitColor = Color.green;
    [FoldoutGroup("Attributes"), SerializeField] private Color offOrbitColor = Color.red;

    private PlanetData planet;
    private Quaternion initialRot;

    private void Start()
    {
        initialRot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = initialRot;
    }

    public void SetMarkerToOnOrbit()
    {
        spriteRenderer.sprite = mainSprite;
        spriteRenderer.color = onOrbitColor;
    }

    public void SetMarkerToOffOrbit()
    {
        spriteRenderer.sprite = offOrbitSprite;
        spriteRenderer.color = offOrbitColor;
    }

    public override void InitializeMarker(Transform _target)
    {
        transform.parent = null;
        transform.localScale = new Vector3(Scale, Scale, 1);
        transform.parent = _target;
        base.InitializeMarker(_target);
    }
}
