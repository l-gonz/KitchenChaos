using System;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable, IObjectHolder
{
    [SerializeField] private Transform holdPoint;

    public ObjectHold ObjectHold { get; private set; }

    /// TEST
    [SerializeField] private KitchenObjectData kitchenObjectData; 
    ////////

    public void Start()
    {
        ObjectHold = new ObjectHold(holdPoint);

        /// TEST
        if (kitchenObjectData != null)
        {
            var test = Instantiate(kitchenObjectData.Prefab);
            ObjectHold.HoldObject(test);
        }
        ////////
    }

    public void Interact(Player player)
    {
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
