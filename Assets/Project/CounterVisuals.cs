using UnityEngine;

public class CounterVisuals : MonoBehaviour
{
    private const string INTERACT_TRIGGER = "OnInteract";
    private IInteractable parentInteractable;

    [SerializeField] private GameObject selectedVisuals;
    [SerializeField] private Animator interactAnimator;

    private void Awake()
    {
        parentInteractable = GetComponentInParent<IInteractable>();
        if (parentInteractable == null)
        {
            Debug.LogError("CounterVisuals: No IInteractable found in parent!");
            enabled = false;
        }
    }

    private void Start()
    {
        OnParentInteractableSelected(null);
        Player.OnSelectedInteractableChanged += OnParentInteractableSelected;
        parentInteractable.OnInteract += OnParentInteracted;
    }

    private void OnParentInteractableSelected(IInteractable interactable)
    {
        if (selectedVisuals != null)
        {
            selectedVisuals.SetActive(parentInteractable == interactable);
        }
    }

    private void OnParentInteracted()
    {
        if (interactAnimator != null)
        {
            interactAnimator.SetTrigger(INTERACT_TRIGGER);
        }
    }
}
