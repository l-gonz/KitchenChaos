
using System;

public interface IInteractable
{
    Action OnInteract { get; set; }
    void Interact(Player player);
}
