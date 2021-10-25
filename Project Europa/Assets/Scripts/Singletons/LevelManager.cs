using UnityEngine;
using Sirenix.OdinInspector;

public class LevelManager : Singleton<LevelManager>
{
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float remainingPlanetsThreshold = 0.5f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int score;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int highScore = 0;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private float roundTime;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanets;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanetsThresholdCount;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool roundActive;

    private SolarSystem solarSystem;
    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    public float RoundTime { get { return roundTime; } }
    public int RemainingPlanets { get { return remainingPlanets; } }
    public bool RoundActive { get { return roundActive; } }

    public delegate void OnRoundStart();
    public OnRoundStart onRoundStart;

    public delegate void OnRoundEnd();
    public OnRoundEnd onRoundEnd;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void OnEnable()
    {
        onRoundStart += SetToPlayerActions;
        onRoundEnd += SetToMenuActions;
    }

    private void Start()
    {
        solarSystem = SolarSystem.Instance;
        remainingPlanetsThresholdCount = Mathf.FloorToInt(solarSystem.PlanetsCount * remainingPlanetsThreshold);
        InputManager.P_Input.MenuAcions.Replay.performed += context => StartRound();
        StartRound();
    }


    private void OnDisable()
    {
        onRoundStart = null;
        onRoundEnd = null;
    }

    public void AddToScore(int _value)
    {
        score += _value;
    }

    public void ReduceRemainingPlanetsByOne()
    {
        remainingPlanets -= 1;
        remainingPlanets = Mathf.Clamp(remainingPlanets, 0, solarSystem.PlanetsCount);
        if(remainingPlanets < remainingPlanetsThresholdCount)
            EndRound();
    }

    private void StartRound()
    {
        if (!roundActive)
        {
            remainingPlanets = solarSystem.PlanetsCount;
            score = 0;
            roundTime = 0;
            roundActive = true;
            onRoundStart?.Invoke();
        }
        else
        {
            Debug.LogWarning("The round is still active");
        }
    }

    private void EndRound()
    {
        if (roundActive)
        {
            if (score > highScore)
                highScore = score;
            onRoundEnd?.Invoke();
            roundActive = false;
        }
        else
        {
            Debug.LogWarning("The round is still inactive");
        }
    }

    private void SetToPlayerActions()
    {
        InputManager.Instance.SetActiveMap(InputManager.ActionMaps.PlayerActions);
    }

    private void SetToMenuActions()
    {
        InputManager.Instance.SetActiveMap(InputManager.ActionMaps.MenuActions);
    }
}
