using UnityEngine;

public class SceneSettings : Singleton<SceneSettings>
{
    [Header("Gameplay")]
    [SerializeField] bool canPause = true;
    public bool CanPause { get => canPause; }

    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.None;
    public CursorLockMode CursorLockMode { get => cursorLockMode; }
        
    [Space]

    [Header("Music")]
    public bool playNewTrack = true;
    public AudioClip soundtrack;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
}
