using UnityEngine;

public interface IInteractable
{
    HoverIcon CurHoverIcon { get; }
    bool InteractionEnabled { get; }

    void Interact();
}

public enum HoverIcon { Default = 0, Eye = 1, Hand = 2, Circle = 3 }