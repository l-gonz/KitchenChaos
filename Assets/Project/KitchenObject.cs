using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectData kitchenObjectData;

    public KitchenObjectData KitchenObjectData => kitchenObjectData;
}
