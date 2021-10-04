using UnityEngine;
using Sirenix.OdinInspector;

public class TetherShot : MonoBehaviour
{
    [FoldoutGroup("Attributes"), SerializeField] private LayerMask targetMask;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(targetMask == (targetMask | (1 << other.gameObject.layer)))
        {
            if(other.tag == "Planet")
            {
                GetComponent<Rigidbody>().velocity *= 0;
                GetComponent<ObjectLifespan>().HaltDestruction();
                transform.position = other.transform.position;
                FixedJoint targetJoint = gameObject.AddComponent<FixedJoint>();
                targetJoint.connectedBody = other.GetComponent<Rigidbody>();
                PlanetaryMovement planetaryMovement = other.GetComponent<PlanetaryMovement>();
                if (planetaryMovement.IsOrbiting)
                    planetaryMovement.StopPlanetaryOrbit();
                else
                    planetaryMovement.StartPlanetaryOrbit();
            }
        }
    }
}
