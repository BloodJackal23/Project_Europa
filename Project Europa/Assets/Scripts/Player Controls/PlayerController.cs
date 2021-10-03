using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetMoveInput());
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private Vector2 GetMoveInput()
    {
        return playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }
}
