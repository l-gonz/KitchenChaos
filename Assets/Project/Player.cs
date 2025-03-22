using UnityEngine;

public class Player : MonoBehaviour
{
    private const float PLAYER_RADIUS = .7f;
    private const float PLAYER_HEIGHT = 2f;
    private const float INTERACT_DISTANCE = 1f;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactLayer;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        MovePlayer();
        Interact();
    }

    private void MovePlayer()
    {
        var inputVector = gameInput.GetMoveInputVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        var moveDistance = moveSpeed * Time.deltaTime;

        IsWalking = moveDirection.magnitude > 0;

        if (!IsWalking) return;
        
        var isObstacleForward = Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, 
            Vector3.Scale(moveDirection, Vector3.forward), moveDistance);
        var isObstacleRight = Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, 
            Vector3.Scale(moveDirection, Vector3.right), moveDistance);

        var collidedMoveDirection = new Vector3(!isObstacleRight ? moveDirection.x : 0, 0,
            !isObstacleForward ? moveDirection.z : 0);
        collidedMoveDirection.Normalize();

        transform.position += collidedMoveDirection * moveDistance;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection,
            rotateSpeed * Time.deltaTime);
    }

    private void Interact()
    {
        var faceDirection = transform.forward.normalized;
        if (Physics.Raycast(transform.position, faceDirection, out var hit, 
            INTERACT_DISTANCE, interactLayer))
        {
            hit.transform.GetComponent<IInteractable>()?.Interact();
        }
    }
}