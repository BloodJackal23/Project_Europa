using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralCelestialLibrary", menuName = "Procedural Data/Procedural Celestial Library")]
public class ProceduralCelestialLibrary : ScriptableObject
{
    [FoldoutGroup("Randomized Attributes"), SerializeField] protected float[] densityRange = new float[2] { 1f, 5f };
    [FoldoutGroup("Randomized Attributes"), SerializeField] protected float[] volumeMultiplierRange = new float[2] { 1f, 10f };

    public float[] DensityRange { get { return densityRange; } }
    public float[] VolumeMultiplierRange { get { return volumeMultiplierRange; } }
}
