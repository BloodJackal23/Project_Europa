using UnityEngine;
using Sirenix.OdinInspector;

public class GameSettings
{
    [FoldoutGroup("Attributes"), SerializeField, Range(-0f, 10f)] public float masterVolume = 6, musicVolume = 4, fxVolume = 4;

    public GameSettings(DefaultGameSettings _defaultSettings)
    {
        masterVolume = _defaultSettings.MasterVolume;
        musicVolume = _defaultSettings.MusicVolume;
        fxVolume = _defaultSettings.FxVolume;
    }

    public GameSettings(float _masterVol, float _musicVol, float _fxVol)
    {
        masterVolume = _masterVol;
        musicVolume = _musicVol;
        fxVolume = _fxVol;
    }

    public static GameSettings DefaultSettings
    {
        get => new GameSettings(.5f, 1f, 1f);
    }
}
