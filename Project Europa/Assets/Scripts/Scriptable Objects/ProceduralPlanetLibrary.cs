using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralPlanetLibrary", menuName = "Procedural Data/Procedural Planet Library")]
public class ProceduralPlanetLibrary : ProceduralCelestialLibrary
{
    [FoldoutGroup("Randomized Attributes"), SerializeField] private Material[] materials;
    [FoldoutGroup("Randomized Attributes"), SerializeField] private string[] planetsNames;

    public Material[] Materials { get { return materials; } }
    public string[] PlanetsNames { get { return planetsNames; } }
}
