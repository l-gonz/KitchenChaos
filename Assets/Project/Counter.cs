using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Counter interacted!");
    }
}
