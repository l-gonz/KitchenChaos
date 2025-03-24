using System;
using UnityEngine;

public interface IObjectHolder
{
    public ObjectHold ObjectHold { get; }
}

public class ObjectHold
{
    private readonly Transform holdPoint;
    private KitchenObject kitchenObject;

    public bool IsHoldingObject => kitchenObject != null;

    public ObjectHold(Transform holdPoint)
    {
        this.holdPoint = holdPoint;
    }

    public void HoldObject(KitchenObject kitchenObject)
    {
        if (IsHoldingObject)
        {
            throw new InvalidOperationException("Counter already holding an object");
        }
        this.kitchenObject = kitchenObject;

        kitchenObject.transform.SetParent(holdPoint);
        kitchenObject.transform.localPosition = Vector3.zero;
    }

    public KitchenObject ReleaseObject()
    {
        if (!IsHoldingObject) return null;

        var releasedObject = kitchenObject;
        kitchenObject = null;
        return releasedObject;
    }
}