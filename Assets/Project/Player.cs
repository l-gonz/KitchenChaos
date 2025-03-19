using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private GameInput gameInput;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        var inputVector = gameInput.GetMoveInputVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        IsWalking = moveDirection.magnitude > 0;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }
}
