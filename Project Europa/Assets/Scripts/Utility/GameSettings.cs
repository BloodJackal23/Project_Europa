
public class GameSettings
{
    public float masterVolume = 6;
    public float musicVolume = 4;
    public float fxVolume = 4;

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
        get
        {
            return new GameSettings(.5f, 1f, 1f);
        }
    }
}
