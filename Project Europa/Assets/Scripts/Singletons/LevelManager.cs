using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class LevelManager : Singleton<LevelManager>
{
    [FoldoutGroup("External References"), SerializeField] private GamePanelDisplay gamePanelDisplay;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float remainingPlanetsThreshold = 0.5f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanets;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanetsThresholdCount;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private bool roundActive;

    private SolarSystem solarSystem;
    private IEnumerator roundCoroutine;
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
        if(gamePanelDisplay == null)
        {
            gamePanelDisplay = FindObjectOfType<GamePanelDisplay>();
        }
        roundCoroutine = RunRoundTimer();
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
        gamePanelDisplay.UpdatePlanetsCount(remainingPlanets);
        StartRound();
    }

    private IEnumerator RunRoundTimer()
    {
        float timer = 0;
        while(roundActive)
        {
            timer += Time.deltaTime;
            gamePanelDisplay.UpdateRemainingTime(timer);
            yield return null;
        }
    }

    public void ReduceRemainingPlanetsByOne()
    {
        remainingPlanets -= 1;
        remainingPlanets = Mathf.Clamp(remainingPlanets, 0, solarSystem.PlanetsCount);
        gamePanelDisplay.UpdatePlanetsCount(remainingPlanets);
        if(remainingPlanets < remainingPlanetsThresholdCount)
            EndRound();
    }

    public void StartRound()
    {
        if (!roundActive)
        {
            roundActive = true;
            onRoundStart?.Invoke();
            StartCoroutine(roundCoroutine);
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
