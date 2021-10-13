using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetRandomizer : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CelestialObjectData celestialObject;
    [FoldoutGroup("Components"), SerializeField] private PlanetaryMovement planetaryMovement;
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetLibrary proceduralPlanetData;

    [FoldoutGroup("Attributes"), SerializeField] private float minDisturbanceForce = 4f;
    [FoldoutGroup("Attributes"), SerializeField] private float maxDisturbanceForce = 10f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float disturbanceChance = 0.2f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 20f)] private float disturbanceInterval = 10f;

    public float MinDisturbanceForce { get { return minDisturbanceForce; } }
    public float MaxDisturbanceForce { get { return maxDisturbanceForce; } }
    public float DisturbanceChance { get { return disturbanceChance; } }
    public float DisturbanceInterval { get { return disturbanceInterval; } }

    private void Start()
    {
        GeneraterandomName();
        SetRandomMaterial();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void GeneraterandomName()
    {
        int rand = Random.Range(0, proceduralPlanetData.PlanetsNames.Length);
        string newName = proceduralPlanetData.PlanetsNames[rand];
        rand = Random.Range(1, 100);
    }

    private void SetRandomMaterial()
    {
        GetComponent<MeshRenderer>().material = proceduralPlanetData.Materials[Random.Range(0, proceduralPlanetData.Materials.Length)];
    }
}
