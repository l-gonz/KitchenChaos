using UnityEngine;

public class CounterVisuals : MonoBehaviour
{
    private IInteractable parentInteractable;

    [SerializeField] private GameObject selectedVisuals;

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
        OnInteractableSelected(null);
        Player.OnSelectedInteractableChanged += OnInteractableSelected;
    }

    private void OnInteractableSelected(IInteractable interactable)
    {
        selectedVisuals.SetActive(parentInteractable == interactable);
    }
}
