using UnityEngine;
using Sirenix.OdinInspector;

public class ScoreManager : Singleton<ScoreManager>
{
    [FoldoutGroup("Attributes"), SerializeField, Range(0f, 1f)] private float remainingPlanetsThreshold = 0.5f;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanets;
    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private int remainingPlanetsThresholdCount;

    private SolarSystem solarSystem;

    public delegate void OnGameWon();
    public OnGameWon onGameWon;

    public delegate void OnGameLost();
    public OnGameLost onGameLost;

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();  
    }

    private void OnEnable()
    {
        onGameWon += WinGame;
        onGameLost += LoseGame;
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
    }

    public void ReduceRemainingPlanetsByOne()
    {
        remainingPlanets -= 1;
        remainingPlanets = Mathf.Clamp(remainingPlanets, 0, solarSystem.PlanetsCount);
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
    }
}
