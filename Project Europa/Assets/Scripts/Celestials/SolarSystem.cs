using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SolarSystem : Singleton<SolarSystem>
{
    public const int MAX_PLANETS_COUNT = 7;

    [FoldoutGroup("Children"), SerializeField] private CelestialObjectData star;
    [FoldoutGroup("Children"), SerializeField] private PlanetData[] planets;
    [FoldoutGroup("Children"), SerializeField] private PlanetaryOrbit[] orbits;

    [FoldoutGroup("Attributes"), SerializeField, Range(1, MAX_PLANETS_COUNT)] int planetsCount;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 1000f)] private float g;
    [FoldoutGroup("Attributes"), SerializeField, Range(-10f, 10f)] private float orbitalOffset = -2.5f;

    private LevelManager levelManager;

    public int PlanetsCount { get { return planetsCount; } }
    public CelestialObjectData Star { get { return star; } }
    public float G { get { return g; } }

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.onRoundStart += InitializePlanets;
        levelManager.onRoundEnd += RemoveAllPlanets;
    }

    private void InitializePlanets()
    {
        if(planetsCount < 7)
        {
            List<PlanetaryOrbit> selectedOrbits = new List<PlanetaryOrbit>();
            for (int i = 0; i < planetsCount; i++)
            {
                int rand = 0;
                do
                    rand = Random.Range(0, MAX_PLANETS_COUNT);
                while (selectedOrbits.Contains(orbits[rand]));
                planets[rand].SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                AssignOrbitToPlanet(orbits[rand], planets[rand]);
                selectedOrbits.Add(orbits[rand]);
            }
        }
        else
        {
            for(int i = 0; i < orbits.Length; i++)
            {
                planets[i].SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                AssignOrbitToPlanet(orbits[i], planets[i]);
            }
                
        }
    }

    private void AssignOrbitToPlanet(PlanetaryOrbit _orbit, PlanetData _planet)
    {
        Vector2 randomLocationOnOrbit = Random.insideUnitCircle.normalized * (_orbit.OuterRadius + orbitalOffset);
        _planet.SetOrbit(_orbit);
        _planet.gameObject.SetActive(true);
        _orbit.gameObject.SetActive(true);
        _planet.transform.position = new Vector3(randomLocationOnOrbit.x, _planet.transform.position.y, randomLocationOnOrbit.y);
    }

    private void RemoveAllPlanets()
    {
        foreach (PlanetData planet in planets)
            planet.gameObject.SetActive(false);
    }
}
