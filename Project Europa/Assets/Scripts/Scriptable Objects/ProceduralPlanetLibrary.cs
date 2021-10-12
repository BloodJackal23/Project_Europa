using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralPlanetLibrary", menuName = "Procedural Data/Procedural Planet Library")]
public class ProceduralPlanetLibrary : ScriptableObject
{
    [FoldoutGroup("Randomized Attributes"), SerializeField] private float[] massRange = new float[2] { 10f, 1000f };
    [FoldoutGroup("Randomized Attributes"), SerializeField] private float[] scaleRange = new float[2] { 1f, 10f };
    [FoldoutGroup("Randomized Attributes"), SerializeField] private float[] orbitalSpeedRange = new float[2] { 1f, 1000000f };
    [FoldoutGroup("Randomized Attributes"), SerializeField] private Material[] materials;
    [FoldoutGroup("Randomized Attributes"), SerializeField] private string[] planetsNames;

    public float[] MassRange { get { return massRange; } }
    public float[] ScaleRange { get { return scaleRange; } }
    public float[] OrbitalSpeedRange { get { return orbitalSpeedRange; } }
    public Material[] Materials { get { return materials; } }
    public string[] PlanetsNames { get { return planetsNames; } }
}
