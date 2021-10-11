using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SolarSystemGenerator : Singleton<SolarSystemGenerator>
{
    [FoldoutGroup("Children"), SerializeField] private CelestialObjectData star;
    [FoldoutGroup("Children"), SerializeField] private PlanetData[] planets;
    [FoldoutGroup("Children"), SerializeField] private PlanetaryOrbit[] orbits;

    [FoldoutGroup("Attributes"), SerializeField, Range(1, 7)] int activeOrbits;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 999999999f)] private float g;

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

    public void InitializeOrbits()
    {
        if(activeOrbits < 7)
        {
            foreach (PlanetData planet in planets)
            {
                planet.gameObject.SetActive(false);
            }

            foreach (PlanetaryOrbit orbit in orbits)
            {
                orbit.gameObject.SetActive(false);
            }

            List<PlanetaryOrbit> selectedOrbits = new List<PlanetaryOrbit>();
            for (int i = 0; i < activeOrbits; i++)
            {
                int rand = 0;
                do
                {
                    rand = Random.Range(0, activeOrbits + 1);
                }
                while (selectedOrbits.Contains(orbits[rand]));
                orbits[rand].SetPlanet(planets[rand]);
                orbits[rand].gameObject.SetActive(true);
                planets[rand].gameObject.SetActive(true);
                selectedOrbits.Add(orbits[rand]);
            }
        }
        else
        {
            for(int i = 0; i < orbits.Length; i++)
            {
                orbits[i].SetPlanet(planets[i]);
            }
        }
    }
}
