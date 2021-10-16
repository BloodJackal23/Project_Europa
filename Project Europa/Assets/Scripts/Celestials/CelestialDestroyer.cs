using UnityEngine;

public class CelestialDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Planet")
        {
            if(other.TryGetComponent(out PlanetData targetPlanet))
            {
                targetPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Destroyed);
            }
        }
    }
}
