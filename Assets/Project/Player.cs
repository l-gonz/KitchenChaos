using System;
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

    private IInteractable selectedInteractable;

    public bool IsWalking { get; private set; }
    public static Action<IInteractable> OnSelectedInteractableChanged;

    private void Start()
    {
        gameInput.OnInteractButtonPressed += Interact;
    }

    private void Update()
    {
        MovePlayer();
        FindInteractables();
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

    private void FindInteractables()
    {
        var faceDirection = transform.forward.normalized;

        if (Physics.Raycast(transform.position, faceDirection, out var hit, INTERACT_DISTANCE, interactLayer) 
            && hit.transform.GetComponent<IInteractable>() is { } interactable)
        {
            SetSelectedInteractable(interactable);
        }
        else
        {
            SetSelectedInteractable(null);
        }
    }

    private void SetSelectedInteractable(IInteractable interactable)
    {
        if (selectedInteractable == interactable) 
            return;
            
        selectedInteractable = interactable;
        OnSelectedInteractableChanged?.Invoke(selectedInteractable);
    }

    private void Interact()
    {
        selectedInteractable?.Interact();
    }
}