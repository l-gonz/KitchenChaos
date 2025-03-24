using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    public Action OnInteractButtonPressed;

    public static GameInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("GameInput: Another instance of GameInput already exists!");
            Destroy(this);
            return;
        }
        
        Instance = this;

        playerInputActions = new();
        playerInputActions.BaseMap.Enable();
        playerInputActions.BaseMap.Interact.performed += ctx => OnInteractButtonPressed?.Invoke();
    }

    public Vector2 GetMoveInputVectorNormalized()
    {
        return playerInputActions.BaseMap.Move.ReadValue<Vector2>().normalized;
    }

}
