using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class LevelManager : Singleton<LevelManager>
{
    [FoldoutGroup("External References"), SerializeField] private GamePanelDisplay gamePanelDisplay;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 600f)] private float levelTime = 120f;
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float remainingPlanetsThreshold = 0.5f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private float remainingTime;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanets;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanetsThresholdCount;

    private SolarSystem solarSystem;
    private IEnumerator timerCoroutine;
    public int RemainingPlanets { get { return remainingPlanets; } }
    public float RemainingTime { get { return remainingTime; } }

    public delegate void OnGameWon();
    public OnGameWon onGameWon;

    public delegate void OnGameLost();
    public OnGameLost onGameLost;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
        if(gamePanelDisplay == null)
        {
            gamePanelDisplay = FindObjectOfType<GamePanelDisplay>();
        }
        timerCoroutine = CountRemainingTime();
    }

    private void OnEnable()
    {
        onGameWon += WinGame;
        onGameLost += LoseGame;
        remainingTime = levelTime;
    }

    private void OnDisable()
    {
        onGameWon = null;
        onGameLost = null;
    }

    private void Start()
    {
        solarSystem = SolarSystem.Instance;
        remainingPlanets = solarSystem.PlanetsCount;
        remainingPlanetsThresholdCount = Mathf.FloorToInt(solarSystem.PlanetsCount * remainingPlanetsThreshold);
        gamePanelDisplay.UpdatePlanetsCount(remainingPlanets);
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator CountRemainingTime()
    {
        float remainingTime = levelTime;
        while(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
                remainingTime = 0;
            gamePanelDisplay.UpdateRemainingTime(remainingTime);
            yield return null;
        }
        onGameWon?.Invoke();
    }

    public void ReduceRemainingPlanetsByOne()
    {
        remainingPlanets -= 1;
        remainingPlanets = Mathf.Clamp(remainingPlanets, 0, solarSystem.PlanetsCount);
        gamePanelDisplay.UpdatePlanetsCount(remainingPlanets);
        if(remainingPlanets < remainingPlanetsThresholdCount)
        {
            onGameLost?.Invoke();
        }
    }

    public void WinGame()
    {
        Debug.Log("Game Won!");
    }

    public void LoseGame()
    {
        Debug.Log("Game Lost!");
        StopCoroutine(timerCoroutine);
    }
}
