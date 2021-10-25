using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    #region Game Data
    public GameSettings gameSettings { get; private set; }
    [SerializeField] private DefaultGameSettings defaultSettings;
    #endregion

    #region Audio Variables
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    public enum AudioChannels { MasterVol, MusicVol, SfxVol };
    [Space]
    #endregion

    private SceneSettings sceneSettings;

    #region Pause System
    public delegate void OnGamePaused();
    public OnGamePaused onGamePaused;

    public delegate void OnGameResumed();
    public OnGamePaused onGameResumed;
    public bool GamePaused { get; set; }
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        GamePaused = false;
        base.Awake();
    }

    private void Start()
    {
        InputManager.P_Input.MenuAcions.Exit.performed += context => QuitGame();
        InitSceneSettings();
        InitGameSettings();
    }

    private void OnDestroy()
    {
        InputManager.P_Input.PlayerActions.Pause.performed -= context => PauseGameToggle();
        InputManager.P_Input.PauseActions.Resume.performed -= context => PauseGameToggle();
        InputManager.P_Input.MenuAcions.Exit.performed -= context => QuitGame();
    }

    public void QuitGame()
    {
        Debug.Log("Game exited...");
        Application.Quit();
    }

    #region Settings Main Methods
    void InitGameSettings()
    {
        LoadGameSettings();
        InitAudioSettings(gameSettings);
    }

    public void SetToDefaultSettings()
    {
        gameSettings = new GameSettings(defaultSettings);
        InitAudioSettings(gameSettings);
        SaveGameSettings();
    }

    public void SaveGameSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", jsonData);
    }

    void LoadGameSettings()
    {
        string path = Application.persistentDataPath + "/GameSettings.json";
        if (File.Exists(path))
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(path));
        else
            SetToDefaultSettings();
    }
    #endregion

    #region Audio Settings Methods
    void InitSceneSettings()
    {
        if(sceneSettings != SceneSettings.Instance)
            sceneSettings = SceneSettings.Instance;
        if (sceneSettings.CanPause)
        {
            InputManager.P_Input.PlayerActions.Pause.performed += context => PauseGameToggle();
            InputManager.P_Input.PauseActions.Resume.performed += context => PauseGameToggle();
        }
        else
        {
            InputManager.P_Input.PlayerActions.Pause.performed -= context => PauseGameToggle();
            InputManager.P_Input.PauseActions.Resume.performed -= context => PauseGameToggle();
        }
        Cursor.lockState = sceneSettings.CursorLockMode;
        InitSceneSoundtrack();
    }

    void InitSceneSoundtrack()
    {
        musicSource.clip = sceneSettings.soundtrack;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void InitAudioSettings(GameSettings _gameSettings)
    {
        SetMasterVolume(GetMasterVolume(_gameSettings));
        SetMusicVolume(GetMusicVolume(_gameSettings));
        SetFXVolume(GetFXVolume(_gameSettings));
    }

    public float GetMasterVolume(GameSettings _gameSettings)
    {
        return _gameSettings.masterVolume;
    }

    public float SetMasterVolume(float _value)
    {
        gameSettings.masterVolume = _value;
        return SetVolume(AudioChannels.MasterVol, _value);
    }

    public float GetMusicVolume(GameSettings _gameSettings)
    {
        return _gameSettings.musicVolume;
    }

    public float SetMusicVolume(float _value)
    {
        gameSettings.musicVolume = _value;
        return SetVolume(AudioChannels.MusicVol, _value);
    }

    public float GetFXVolume(GameSettings _gameSettings)
    {
        return _gameSettings.fxVolume;
    }

    public float SetFXVolume(float _value)
    {
        gameSettings.fxVolume = _value;
        return SetVolume(AudioChannels.SfxVol, _value);
    }

    float SetVolume(AudioChannels _channel, float _value)
    {
        audioMixer.SetFloat(_channel.ToString(), Mathf.Log10(_value) * 20);
        return _value;
    }

    public IEnumerator FadeOut(float _fadeTime)
    {
        Debug.Log("Fade out");
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / _fadeTime;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }

    #endregion

    #region Game Pause
    public void PauseGameToggle()
    {
        GamePaused = !GamePaused;
        if (GamePaused)
        {
            Time.timeScale = 0;
            Debug.Log("Game Paused!");
            Cursor.lockState = CursorLockMode.None;
            onGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Game Resumed!");
            Cursor.lockState = sceneSettings.CursorLockMode;
            onGameResumed?.Invoke();
        }
    }
    #endregion
}
