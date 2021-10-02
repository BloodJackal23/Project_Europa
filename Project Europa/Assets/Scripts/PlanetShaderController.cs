using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetShaderController : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private Material planetMaterial;

    void Start()
    {
        float random = Random.Range(0f, 1f);
        planetMaterial.SetFloat("WaterLevel", random);
        random = Random.Range(-100f, 100f);
        planetMaterial.SetFloat("LandOffset", random);
    }
}
