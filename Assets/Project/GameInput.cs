using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new();
        playerInputActions.BaseMap.Enable();
    }

    public Vector2 GetMoveInputVectorNormalized()
    {
        return playerInputActions.BaseMap.Move.ReadValue<Vector2>().normalized;
    }
}
