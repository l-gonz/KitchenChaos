using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    public Action OnInteractButtonPressed;

    private void Awake()
    {
        playerInputActions = new();
        playerInputActions.BaseMap.Enable();
        playerInputActions.BaseMap.Interact.performed += ctx => OnInteractButtonPressed?.Invoke();
    }

    public Vector2 GetMoveInputVectorNormalized()
    {
        return playerInputActions.BaseMap.Move.ReadValue<Vector2>().normalized;
    }

}
