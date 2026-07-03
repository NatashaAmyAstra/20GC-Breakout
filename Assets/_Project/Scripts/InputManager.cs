using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static InputSystem_Actions _inputSystem = new();

    public static float PlayerXInputValue;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Setup()
    {
        _inputSystem.Enable();

        _inputSystem.Player.Move.performed += OnPlayerMoveInput;
    }


    private static void OnPlayerMoveInput(InputAction.CallbackContext value)
    {
        PlayerXInputValue = value.ReadValue<float>();
    }
}
