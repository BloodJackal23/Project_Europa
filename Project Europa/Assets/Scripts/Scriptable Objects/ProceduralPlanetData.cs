using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetShaderData", menuName = "Procedural Data/Procedural Planet Data")]
public class ProceduralPlanetData : ScriptableObject
{
    [FoldoutGroup("Randomized Attributes"), SerializeField] private Material[] materials;
    [FoldoutGroup("Randomized Attributes"), SerializeField] private string[] planetsNames;

    public Material[] Materials { get { return materials; } }
    public string[] PlanetsNames { get { return planetsNames; } }
}
