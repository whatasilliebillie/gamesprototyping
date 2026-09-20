using UnityEngine;

public interface IInteractable
{
    HoverIcon hoverIcon { get; }

    void SetHover(bool toggle);
    void Interact();
}

public enum HoverIcon { Default = 0, Eye = 1, Hand = 2, Circle = 3 }