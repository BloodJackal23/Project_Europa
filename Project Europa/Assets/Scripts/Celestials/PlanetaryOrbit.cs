using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class PlanetaryOrbit : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private MeshRenderer meshRenderer;
    [FoldoutGroup("Attributes"), SerializeField] private Material clearMat;
    [FoldoutGroup("Attributes"), SerializeField] private Material dangerMat;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 900f)] private float outerRadius;
    [FoldoutGroup("Attributes"), SerializeField, Range(1f, 20f)] private float orbitThickness;

    private PlanetData orbitingPlanet;
    private float innerRadius;
    //private PlanetRandomizer randomizer;
    public PlanetData OrbitingPlanet { get { return orbitingPlanet; } }
    public float OuterRadius { get { return outerRadius; } }
    public float OrbitThickness { get { return orbitThickness; } }

    private void Awake()
    {
        innerRadius = Mathf.Max(0, outerRadius - orbitThickness);
    }

    private void FixedUpdate()
    {
        if(orbitingPlanet != null && orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Destroyed)
        {
            float sqrMag = Vector3.SqrMagnitude(orbitingPlanet.transform.position - SolarSystemGenerator.Instance.transform.position);
            if(sqrMag < outerRadius * outerRadius && sqrMag > innerRadius * innerRadius)
            {
                Debug.Log("Planet: " + orbitingPlanet.gameObject.name + " is in orbit!");
                orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
                orbitingPlanet.RigidBody.velocity *= 0;
                orbitingPlanet.onOrbitEnter?.Invoke();
                meshRenderer.material = clearMat;
                //StartCoroutine(RunDisturbance());
            }
            else
            {
                Debug.Log("Planet: " + orbitingPlanet.gameObject.name + " is out of orbit!");
                if (orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
                    orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                meshRenderer.material = dangerMat;
            }
        } 
    }

    public void SetOrbitingPlanet(PlanetData _targetPlanet)
    {
        orbitingPlanet = _targetPlanet;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.zero, Mathf.Max(0, outerRadius - orbitThickness));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, outerRadius);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out PlanetData planet))
    //    {
    //        Debug.Log("Planet: " + other.gameObject.name + " is in orbit!");
    //        if (orbitingPlanet == null)
    //            orbitingPlanet = planet;

    //        if (planet == orbitingPlanet)
    //        {
    //            orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
    //            orbitingPlanet.RigidBody.velocity *= 0;
    //            orbitingPlanet.onOrbitEnter?.Invoke();
    //            meshRenderer.material = clearMat;
    //            //StartCoroutine(RunDisturbance());
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.TryGetComponent(out PlanetData planet))
    //    {
    //        Debug.Log("Planet: " + other.gameObject.name + " is out of orbit!");
    //        if (planet == orbitingPlanet)
    //        {

    //            if (orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
    //                orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
    //            meshRenderer.material = dangerMat;
    //            //randomizer = null;
    //            //StopAllCoroutines();
    //        }
    //    }
    //}

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
