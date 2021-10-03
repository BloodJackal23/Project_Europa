using UnityEngine;

[CreateAssetMenu(fileName = "Default Game Settings", menuName = "Utility/Game Settings")]
public class DefaultGameSettings : ScriptableObject
{
    [Header("Audio Settings")]
    [SerializeField] private float masterVolume = 6;
    public float MasterVolume { get => masterVolume; }

    [SerializeField] private float musicVolume = 4;
    public float MusicVolume { get => musicVolume; }

    [SerializeField] private float fxVolume = 4;
    public float FxVolume { get => fxVolume; }

}
