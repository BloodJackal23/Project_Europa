using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetaryOrbit : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private MeshRenderer meshRenderer;
    [FoldoutGroup("Attributes"), SerializeField] private Material clearMat;
    [FoldoutGroup("Attributes"), SerializeField] private Material dangerMat;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 900f)] private float outerRadius;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 20f)] private float orbitThickness;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private float innerRadius;

    private PlanetData orbitingPlanet;

    public delegate void OnOrbitEnter();
    public OnOrbitEnter onOrbitEnter;

    public delegate void OnOrbitExit();
    public OnOrbitExit onOrbitExit;

    public PlanetData OrbitingPlanet { get { return orbitingPlanet; } }
    public float OuterRadius { get { return outerRadius; } }
    public float InnerRadius { get { return innerRadius; } }
    public float OrbitThickness { get { return orbitThickness; } }

    private void Awake()
    {
        innerRadius = Mathf.Max(0, outerRadius - orbitThickness);
    }

    private void OnEnable()
    {
        onOrbitEnter += SetMaterialToClear;
        onOrbitExit += SetMaterialToDanger;
    }

    private void OnDisable()
    {
        onOrbitEnter -= SetMaterialToClear;
        onOrbitExit -= SetMaterialToDanger;
    }

    private void SetMaterialToClear()
    {
        meshRenderer.material = clearMat;
    }

    private void SetMaterialToDanger()
    {
        meshRenderer.material = dangerMat;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.Max(0, outerRadius - orbitThickness));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, outerRadius);
    }
}
