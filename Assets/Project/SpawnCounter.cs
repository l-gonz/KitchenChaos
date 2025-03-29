using System;
using UnityEngine;

public class SpawnCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectData kitchenObjectData;

    public Action OnInteract { get; set; }

    public void Interact(Player player)
    {
        if (player.ObjectHold.IsHoldingObject) return;

        OnInteract?.Invoke();

        var kitchenObject = Instantiate(kitchenObjectData.Prefab);
        player.ObjectHold.HoldObject(kitchenObject);
    }
}