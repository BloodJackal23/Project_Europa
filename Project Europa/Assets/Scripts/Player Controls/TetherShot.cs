using UnityEngine;
using Sirenix.OdinInspector;

public class TetherShot : MonoBehaviour
{
    [FoldoutGroup("Attributes"), SerializeField] private LayerMask targetMask;

    private PlanetData tetherTarget;

    public delegate void OnHit(Rigidbody _target, TetherShot _shot);
    public OnHit onHit;
    public delegate void OnDetach();
    public OnDetach onDetach;

    private void OnTriggerEnter(Collider other)
    {
        if(tetherTarget == null && targetMask == (targetMask | (1 << other.gameObject.layer)))
        {
            if(other.tag == "Planet")
            {
                OnTargetHit(other);
            }
        }
    }

    private void OnDisable()
    {
        onHit = null;
        onDetach = null;
    }

    private void OnTargetHit(Collider _targetCol)
    {
        GetComponent<Rigidbody>().velocity *= 0;
        GetComponent<ObjectLifespan>().PauseDestruction();
        transform.position = _targetCol.transform.position;
        gameObject.AddComponent<FixedJoint>().connectedBody = _targetCol.attachedRigidbody;
        tetherTarget = _targetCol.GetComponent<PlanetData>();
        tetherTarget.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Tethered);
        onHit?.Invoke(_targetCol.attachedRigidbody, this); tetherTarget = _targetCol.GetComponent<PlanetData>();
    }

    public void DisconnectTether()
    {
        onDetach?.Invoke();
        if (tetherTarget.ObjectStaus == CelestialObjectData.CelestialObjectStaus.Tethered)
            tetherTarget.SetObjectStatus(CelestialObjectData.CelestialObjectStaus.Drifting);
        Destroy(gameObject);
    }
}
