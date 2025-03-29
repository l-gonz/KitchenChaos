using System;
using UnityEngine;

public class EmptyCounter : MonoBehaviour, IInteractable, IObjectHolder
{
    [SerializeField] private Transform holdPoint;

    public ObjectHold ObjectHold { get; private set; }
    public Action OnInteract { get; set; }

    public void Start()
    {
        ObjectHold = new ObjectHold(holdPoint);
    }

    public void Interact(Player player)
    {
        OnInteract?.Invoke();
        
        if (!ObjectHold.IsHoldingObject && player.ObjectHold.IsHoldingObject)
        {
            ObjectHold.HoldObject(player.ObjectHold.ReleaseObject());
        }
        else if (ObjectHold.IsHoldingObject && !player.ObjectHold.IsHoldingObject)
        {
            player.ObjectHold.HoldObject(ObjectHold.ReleaseObject());
        }
    }
}
