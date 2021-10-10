using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class PlanetRandomizer : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CelestialObject celestialObject;
    [FoldoutGroup("Components"), SerializeField] private PlanetaryMovement planetaryMovement;
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetData proceduralPlanetData;

    [FoldoutGroup("Attributes"), SerializeField] private float minDisturbanceForce = 4f;
    [FoldoutGroup("Attributes"), SerializeField] private float maxDisturbanceForce = 10f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float disturbanceChance = 0.2f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 20f)] private float disturbanceInterval = 10f;

    public float MinDisturbanceForce { get { return minDisturbanceForce; } }
    public float MaxDisturbanceForce { get { return maxDisturbanceForce; } }
    public float DisturbanceChance { get { return disturbanceChance; } }
    public float DisturbanceInterval { get { return disturbanceInterval; } }

    private void Awake()
    {
        SetRandomOrbitDirection();
    }

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
        celestialObject.SetName(newName + "-" + rand.ToString("000"));
    }

    private void SetRandomMaterial()
    {
        GetComponent<MeshRenderer>().material = proceduralPlanetData.Materials[Random.Range(0, proceduralPlanetData.Materials.Length)];
    }

    private void SetRandomOrbitDirection()
    {
        int randDirection = Random.Range(0, 2);
        if (randDirection == 0)
            planetaryMovement.SetOrbitDirection(false);
        else
            planetaryMovement.SetOrbitDirection(true);
    }
}
