using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetRandomizer : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CelestialObject celestialObject;
    [FoldoutGroup("Components"), SerializeField] private PlanetaryOrbit planetaryOrbit;
    [FoldoutGroup("Components"), SerializeField] private ProceduralPlanetData proceduralPlanetData;

    private void Awake()
    {
        SetRandomOrbitDirection();
    }

    private void Start()
    {
        GeneraterandomName();
        SetRandomMaterial();
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
            planetaryOrbit.SetOrbitDirection(false);
        else
            planetaryOrbit.SetOrbitDirection(true);
    }
}
