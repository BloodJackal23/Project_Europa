using UnityEngine;
using Sirenix.OdinInspector;

public class InputManager : Singleton<InputManager>
{
    public enum ActionMaps { PlayerActions, MenuActions, PauseActions}

    [FoldoutGroup("Read Only"), SerializeField, ReadOnly] private ActionMaps activeMap;
    private static PlayerInput playerInput;
    public static PlayerInput P_Input
    {
        get
        {
            if (playerInput == null)
                playerInput = new PlayerInput();
            return playerInput;
        }
    }

    public ActionMaps ActiveMap { get { return activeMap; } }

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();
        playerInput = new PlayerInput();
    }

    public void SetActiveMap(ActionMaps _map)
    {
        switch (activeMap)
        {
            case ActionMaps.PlayerActions:
                playerInput.PlayerActions.Disable();
                break;
            case ActionMaps.MenuActions:
                playerInput.MenuAcions.Disable();
                break;
            case ActionMaps.PauseActions:
                playerInput.PauseActions.Disable();
                break;
        }
        
        switch (_map)
        {
            case ActionMaps.PlayerActions:
                playerInput.PlayerActions.Enable();
                break;
            case ActionMaps.MenuActions:
                playerInput.MenuAcions.Enable();
                break;
            case ActionMaps.PauseActions:
                playerInput.PauseActions.Enable();
                break;
        }
        activeMap = _map;
    }
}
