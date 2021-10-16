using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SolarSystemGenerator : Singleton<SolarSystemGenerator>
{
    [FoldoutGroup("Children"), SerializeField] private CelestialObjectData star;
    [FoldoutGroup("Children"), SerializeField] private PlanetData[] planets;
    [FoldoutGroup("Children"), SerializeField] private PlanetaryOrbit[] orbits;

    [FoldoutGroup("Attributes"), SerializeField, Range(1, 7)] int activeOrbits;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 9999999f)] private float g;
    [FoldoutGroup("Attributes"), SerializeField, Range(-10f, 10f)] private float orbitalOffset = -2.5f;

    public CelestialObjectData Star { get { return star; } }
    public float G { get { return g; } }

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void Start()
    {
        InitializeOrbits();
    }

    private void InitializeOrbits()
    {
        if(activeOrbits < 7)
        {
            List<PlanetaryOrbit> selectedOrbits = new List<PlanetaryOrbit>();
            for (int i = 0; i < activeOrbits; i++)
            {
                int rand = 0;
                do
                    rand = Random.Range(0, activeOrbits);
                while (selectedOrbits.Contains(orbits[rand]));
                AssignPlanetToOrbit(orbits[rand], planets[rand]);
                selectedOrbits.Add(orbits[rand]);
            }
        }
        else
        {
            for(int i = 0; i < orbits.Length; i++)
                AssignPlanetToOrbit(orbits[i], planets[i]);
        }
    }

    private void AssignPlanetToOrbit(PlanetaryOrbit _orbit, PlanetData _planet)
    {
        Vector2 randomLocationOnOrbit = Random.insideUnitCircle.normalized * (_orbit.Radius + orbitalOffset);
        _planet.gameObject.SetActive(true);
        _orbit.gameObject.SetActive(true);
        _planet.transform.position = new Vector3(randomLocationOnOrbit.x, _planet.transform.position.y, randomLocationOnOrbit.y);
        //_orbit.SetPlanet(_planet);
    }
}
