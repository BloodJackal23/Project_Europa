using UnityEngine;
using Sirenix.OdinInspector;

public class TetherShot : MonoBehaviour
{
    public delegate void OnHit(Rigidbody _target);
    public OnHit onHit;

    [FoldoutGroup("Attributes"), SerializeField] private LayerMask targetMask;

    private PlanetaryMovement tetherTarget;

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
                targetJoint.connectedBody = other.attachedRigidbody;
                tetherTarget = other.GetComponent<PlanetaryMovement>();
                tetherTarget.SetTethered(true);
                tetherTarget.onDetached += DestroyOnDetachment;
                tetherTarget.StopPlanetaryOrbit();
                onHit?.Invoke(other.attachedRigidbody);
            }
        }
    }

    private void OnDisable()
    {
        onHit = null;
    }

    private void DestroyOnDetachment()
    {
        tetherTarget.SetTethered(false);
        tetherTarget.onDetached -= DestroyOnDetachment;
        Destroy(gameObject);
    }
}
