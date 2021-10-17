using UnityEngine;

public class CelestialDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlanetData targetPlanet))
        {
            targetPlanet.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Destroyed);
            targetPlanet.onDestroyed?.Invoke();
        }
    }
}
