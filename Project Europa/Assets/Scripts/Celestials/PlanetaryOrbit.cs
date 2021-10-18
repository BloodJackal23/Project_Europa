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
    private bool addedOrbitalVelocity;
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
                if(orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
                {
                    Debug.Log("Planet: " + orbitingPlanet.gameObject.name + " is in orbit!");

                    if (!addedOrbitalVelocity)
                    {
                        orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Orbiting);
                        orbitingPlanet.RigidBody.velocity *= 0;
                        orbitingPlanet.onOrbitEnter?.Invoke();
                        addedOrbitalVelocity = true;
                    }
                }
                meshRenderer.material = clearMat;
            }
            else
            {
                Debug.Log("Planet: " + orbitingPlanet.gameObject.name + " is out of orbit!");
                if(orbitingPlanet.ObjectStaus != CelestialObjectData.CelestialObjectStaus.Tethered)
                    orbitingPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
                meshRenderer.material = dangerMat;
                addedOrbitalVelocity = false;
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
}
