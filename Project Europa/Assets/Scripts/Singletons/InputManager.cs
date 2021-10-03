using UnityEngine;

public class InputManager : MonoBehaviour
{
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

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
}
