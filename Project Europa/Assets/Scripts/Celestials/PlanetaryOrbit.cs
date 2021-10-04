using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class PlanetaryOrbit : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private MeshRenderer meshRenderer;
    [FoldoutGroup("Attributes"), SerializeField] private Material clearMat;
    [FoldoutGroup("Attributes"), SerializeField] private Material dangerMat;

    private CelestialObject orbitingPlanet;
    private Collider planetCollider;
    private PlanetaryMovement planetaryMovement;
    private PlanetRandomizer randomizer;
    public CelestialObject OrbitingPlanet { get { return orbitingPlanet; } }

    public void SetPlanet(CelestialObject _planet)
    {
        orbitingPlanet = _planet;
        planetCollider = orbitingPlanet.GetComponent<Collider>();
        planetaryMovement = orbitingPlanet.GetComponent<PlanetaryMovement>();
        randomizer = orbitingPlanet.GetComponent<PlanetRandomizer>();
        if (planetaryMovement.IsTethered)
        {
            planetaryMovement.onDetached?.Invoke();
        }
        planetaryMovement.StartPlanetaryOrbit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(orbitingPlanet == null && other.gameObject.tag == "Planet")
        {
            orbitingPlanet = other.GetComponent<CelestialObject>();
            SetPlanet(orbitingPlanet);        
            meshRenderer.material = clearMat;
            StartCoroutine(RunDisturbance());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == planetCollider)
        {
            planetaryMovement.StopPlanetaryOrbit();
            meshRenderer.material = dangerMat;
            orbitingPlanet = null;
            planetCollider = null;
            planetaryMovement = null;
            randomizer = null;
            StopAllCoroutines();
        }
    }

    private IEnumerator RunDisturbance()
    {
        while (planetaryMovement.IsOrbiting)
        {
            yield return new WaitForSeconds(randomizer.DisturbanceInterval);
            float rand = Random.Range(0f, 1f);
            if (rand < randomizer.DisturbanceChance)
            {
                orbitingPlanet.RigidBody.AddForce(Random.insideUnitSphere * Random.Range(randomizer.MinDisturbanceForce, randomizer.MaxDisturbanceForce), ForceMode.Impulse);
            }
        }
    }
}
