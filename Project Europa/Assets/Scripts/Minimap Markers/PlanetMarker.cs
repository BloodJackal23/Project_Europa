using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetMarker : MovingObjectMarker
{
    [FoldoutGroup("Attributes"), SerializeField] private Sprite offOrbitSprite;
    [FoldoutGroup("Attributes"), SerializeField] private Color onOrbitColor = Color.green;
    [FoldoutGroup("Attributes"), SerializeField] private Color offOrbitColor = Color.red;

    private PlanetData planet;
    public override void InitializeMarker(Transform _target)
    {
        base.InitializeMarker(_target);
        if (targetObject.TryGetComponent(out planet))
        {
            planet.onDestroyed += OnTargetDestroyed;
            planet.Orbit.onOrbitEnter += SetMarkerToOnOrbit;
            planet.Orbit.onOrbitExit += SetMarkerToOffOrbit;
        }
    }

    protected override void OnTargetDestroyed()
    {
        planet.onDestroyed -= OnTargetDestroyed;
        planet.Orbit.onOrbitEnter -= SetMarkerToOnOrbit;
        planet.Orbit.onOrbitExit -= SetMarkerToOffOrbit;
        base.OnTargetDestroyed();
    }

    private void SetMarkerToOnOrbit()
    {
        spriteRenderer.sprite = mainSprite;
        spriteRenderer.color = onOrbitColor;
    }

    private void SetMarkerToOffOrbit()
    {
        spriteRenderer.sprite = offOrbitSprite;
        spriteRenderer.color = offOrbitColor;
    }
}
