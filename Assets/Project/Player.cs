using UnityEngine;

public class Player : MonoBehaviour
{
    private const float PLAYER_RADIUS = .7f;
    private const float PLAYER_HEIGHT = 2f;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }

    private void Update()
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

        if (isObstacleForward && isObstacleRight) return;

        var collidedMoveDirection = new Vector3(!isObstacleRight ? moveDirection.x : 0, 0,
            !isObstacleForward ? moveDirection.z : 0);
        collidedMoveDirection.Normalize();

        transform.position += collidedMoveDirection * moveDistance;
        transform.forward = Vector3.Slerp(transform.forward, collidedMoveDirection,
            rotateSpeed * Time.deltaTime);
    }
}
