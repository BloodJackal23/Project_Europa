using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CelestialObject))]
public class CelestialMovement : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] protected CelestialObject celestialObject;
    protected virtual void Awake()
    {
        if (!celestialObject)
            celestialObject = GetComponent<CelestialObject>();
    }
}
