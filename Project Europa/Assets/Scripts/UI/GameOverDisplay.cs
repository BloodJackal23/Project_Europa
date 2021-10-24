using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class GameOverDisplay : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CanvasGroup canvasGroup;
    [FoldoutGroup("Components"), SerializeField] private TMP_Text roundScoreText;
    [FoldoutGroup("Components"), SerializeField] private TMP_Text highScoreText;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.onRoundStart += HideGameOver;
        levelManager.onRoundEnd += DisplayGameOver;
    }

    private void DisplayGameOver()
    {
        SetCanvasGroupValues(1, true, true);
        roundScoreText.text = levelManager.Score.ToString("0000");
    }

    private void HideGameOver()
    {
        SetCanvasGroupValues(0, false, false);
    }

    private void SetCanvasGroupValues(float _alpha, bool _interactable, bool _blockRaycasts)
    {
        canvasGroup.alpha = _alpha;
        canvasGroup.interactable = _interactable;
        canvasGroup.blocksRaycasts = _blockRaycasts;
    }
}
