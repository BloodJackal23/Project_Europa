using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class GamePanelDisplay : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private TMP_Text remainingTimeCounterText;
    [FoldoutGroup("Components"), SerializeField] private TMP_Text remainingPlanetsCountText;

    public void UpdateRemainingTime(float _seconds)
    {
        int minutes = Mathf.FloorToInt(_seconds / 60f);
        float seconds = _seconds % 60f;
        remainingTimeCounterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void UpdatePlanetsCount(int _value)
    {
        remainingPlanetsCountText.text = _value.ToString("00");
    }
}
