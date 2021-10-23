using Sirenix.OdinInspector;
using UnityEngine;

public class PlanetScoreTracker : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private PlanetData planetData;
    [FoldoutGroup("Attributes"), SerializeField, Range(1, 10)] private int onOrbitMultiplier = 5;
    [FoldoutGroup("Attributes"), SerializeField, Range(0.1f, 5f)] private float scoreUpdateInterval = 1f;

    private LevelManager levelManager;
    private float scoreTimer;

    private void Start()
    {
        levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
        scoreTimer = 0;
    }

    private void Update()
    {
        if (levelManager.RoundActive)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer > scoreUpdateInterval)
            {
                if (planetData.ObjectStaus == CelestialObjectData.CelestialObjectStaus.Orbiting)
                {
                    levelManager.AddToScore(onOrbitMultiplier);
                }
                else
                {
                    levelManager.AddToScore(1);
                }
                scoreTimer = 0;
            }
        }
    }
}
