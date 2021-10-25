using UnityEngine;
using Sirenix.OdinInspector;

public class PausePanel : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        GameManager.Instance.onGamePaused += ShowPausePanel;
        GameManager.Instance.onGameResumed += HidePausePanel;
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGamePaused -= ShowPausePanel;
        GameManager.Instance.onGameResumed -= HidePausePanel;
    }

    private void ShowPausePanel()
    {
        InputManager.Instance.SetActiveMap(InputManager.ActionMaps.PauseActions);
        SetCanvasGroupValues(1, true, true);
    }

    private void HidePausePanel()
    {
        InputManager.Instance.SetActiveMap(InputManager.ActionMaps.PlayerActions);
        SetCanvasGroupValues(0, false, false);
    }

    private void SetCanvasGroupValues(float _alpha, bool _interactable, bool _blockRaycasts)
    {
        canvasGroup.alpha = _alpha;
        canvasGroup.interactable = _interactable;
        canvasGroup.blocksRaycasts = _blockRaycasts;
    }
}
