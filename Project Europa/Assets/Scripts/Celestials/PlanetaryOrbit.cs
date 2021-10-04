using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class PlanetaryOrbit : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private MeshRenderer meshRenderer;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == planetCollider)
        {
            if (planetaryMovement.IsTethered)
            {
                planetaryMovement.onDetached?.Invoke();
            }
            meshRenderer.enabled = false;
            planetaryMovement.StartPlanetaryOrbit();
            StartCoroutine(RunDisturbance());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == planetCollider)
        {
            meshRenderer.enabled = true;
            planetaryMovement.StopPlanetaryOrbit();
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
                Debug.Log("Random force add to " + gameObject.name);
                Debug.Log("Random = " + rand.ToString());
            }
        }
    }
}
