using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static event EventHandler OnShootPerformed;
    private static InputSystem_Actions _inputSystem = new();

    public static float PlayerXInputValue;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Setup()
    {
        _inputSystem.Enable();

        _inputSystem.Player.Move.performed += OnPlayerMoveInput;
        _inputSystem.Player.Shoot.performed += OnPlayerShoot;
    }


    private static void OnPlayerMoveInput(InputAction.CallbackContext value)
    {
        PlayerXInputValue = value.ReadValue<float>();
    }

    private static void OnPlayerShoot(InputAction.CallbackContext value)
    {
        OnShootPerformed?.Invoke(null, EventArgs.Empty);
    }
}
