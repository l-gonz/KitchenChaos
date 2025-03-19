using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING_TRIGGER = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING_TRIGGER, player.IsWalking);
    }
}
