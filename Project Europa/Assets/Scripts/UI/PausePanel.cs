using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [FoldoutGroup("Components"), SerializeField] private CanvasGroup canvasGroup;
    [FoldoutGroup("Components"), SerializeField] private Button quitButton;
    [FoldoutGroup("Components"), SerializeField] private Slider volSlider;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onGamePaused += ShowPausePanel;
        gameManager.onGameResumed += HidePausePanel;
        quitButton.onClick.AddListener(delegate { gameManager.QuitGame(); });
        volSlider.onValueChanged.AddListener(delegate { gameManager.SetMasterVolume(volSlider.value); });
        volSlider.value = gameManager.GetMasterVolume(gameManager.gameSettings);
        if (!gameManager.GamePaused)
        {
            HidePausePanel();
        }
    }

    private void OnDestroy()
    {
        gameManager.onGamePaused -= ShowPausePanel;
        gameManager.onGameResumed -= HidePausePanel;
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
