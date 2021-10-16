using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class PlanetaryOrbit : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private MeshRenderer meshRenderer;
    [FoldoutGroup("Attributes"), SerializeField] private Material clearMat;
    [FoldoutGroup("Attributes"), SerializeField] private Material dangerMat;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 900f)] private float radius;

    private PlanetData orbitingPlanet;
    private Collider planetCollider;
    private PlanetaryMovement planetaryMovement;
    //private PlanetRandomizer randomizer;
    public PlanetData OrbitingPlanet { get { return orbitingPlanet; } }
    public float Radius { get { return radius; } }

    public void SetPlanet(PlanetData _planet)
    {
        orbitingPlanet = _planet;
        planetCollider = orbitingPlanet.GetComponent<Collider>();
        planetaryMovement = orbitingPlanet.GetComponent<PlanetaryMovement>();
        planetaryMovement.StartPlanetaryOrbit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(orbitingPlanet == null && other.gameObject.tag == "Planet")
        {
            SetPlanet(other.GetComponent<PlanetData>());        
            meshRenderer.material = clearMat;
            //StartCoroutine(RunDisturbance());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == planetCollider)
        {
            if (orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
                orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
            meshRenderer.material = dangerMat;
            orbitingPlanet = null;
            planetCollider = null;
            planetaryMovement = null;
            //randomizer = null;
            //StopAllCoroutines();
        }
    }

    //private IEnumerator RunDisturbance()
    //{
    //    while (planetaryMovement.IsOrbiting)
    //    {
    //        yield return new WaitForSeconds(randomizer.DisturbanceInterval);
    //        float rand = Random.Range(0f, 1f);
    //        if (rand < randomizer.DisturbanceChance)
    //        {
    //            orbitingPlanet.RigidBody.AddForce(Random.insideUnitSphere * Random.Range(randomizer.MinDisturbanceForce, randomizer.MaxDisturbanceForce), ForceMode.Impulse);
    //        }
    //    }
    //}
}
