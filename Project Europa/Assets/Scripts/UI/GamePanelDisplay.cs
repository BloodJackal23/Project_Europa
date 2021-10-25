using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System.Collections;

public class GamePanelDisplay : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private TMP_Text scoreCounterText;
    [FoldoutGroup("Components"), SerializeField] private TMP_Text remainingTimeCounterText;
    [FoldoutGroup("Components"), SerializeField] private TMP_Text remainingPlanetsCountText;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.onRoundStart += StartNewRound;
    }

    private void OnDisable()
    {
        levelManager.onRoundStart -= StartNewRound;
    }

    private void StartNewRound()
    {
        UpdateScore(0);
        UpdateTime(0);
        UpdatePlanetsCount(levelManager.RemainingPlanets);
        StartCoroutine(RunRoundData());
    }

    private void UpdateScore(int _value)
    {
        scoreCounterText.text = _value.ToString("0000");
    }

    private void UpdateTime(float _seconds)
    {
        int minutes = Mathf.FloorToInt(_seconds / 60f);
        int seconds = Mathf.FloorToInt(_seconds % 60f);
        remainingTimeCounterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void UpdatePlanetsCount(int _value)
    {
        remainingPlanetsCountText.text = _value.ToString("00");
    }

    private IEnumerator RunRoundData()
    {
        float timer = 0;
        while (levelManager.RoundActive)
        {
            timer += Time.deltaTime;
            UpdateScore(levelManager.Score);
            UpdateTime(timer);
            UpdatePlanetsCount(levelManager.RemainingPlanets);
            yield return null;
        }
        UpdatePlanetsCount(levelManager.RemainingPlanets);
    }
}
