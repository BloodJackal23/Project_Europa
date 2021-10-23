using UnityEngine;
using Sirenix.OdinInspector;

public class LevelManager : Singleton<LevelManager>
{
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float remainingPlanetsThreshold = 0.5f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int score;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanets;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanetsThresholdCount;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool roundActive;

    private SolarSystem solarSystem;
    public int Score { get { return score; } }
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
        onRoundStart += OnRoundStarted;
        onRoundEnd += OnRoundEnded;
    }

    private void OnDisable()
    {
        onRoundStart = null;
        onRoundEnd = null;
    }

    private void Start()
    {
        solarSystem = SolarSystem.Instance;
        remainingPlanets = solarSystem.PlanetsCount;
        remainingPlanetsThresholdCount = Mathf.FloorToInt(solarSystem.PlanetsCount * remainingPlanetsThreshold);
        StartRound();
    }

    public void ReduceRemainingPlanetsByOne()
    {
        remainingPlanets -= 1;
        remainingPlanets = Mathf.Clamp(remainingPlanets, 0, solarSystem.PlanetsCount);
        if(remainingPlanets < remainingPlanetsThresholdCount)
            EndRound();
    }

    public void StartRound()
    {
        if (!roundActive)
        {
            roundActive = true;
            onRoundStart?.Invoke();
        }
    }

    private void OnRoundStarted()
    {
        Debug.Log("Round Started");
    }

    public void EndRound()
    {
        if (roundActive)
        {
            onRoundEnd?.Invoke();
            roundActive = false;
        }
    }

    private void OnRoundEnded()
    {
        Debug.Log("Round Ended");
    }
}
