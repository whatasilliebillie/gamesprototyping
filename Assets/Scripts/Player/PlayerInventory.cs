using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [SerializeField] private List<ItemScriptable> items = new List<ItemScriptable>();

    public event Action OnInventoryChanged;
    public IReadOnlyList<ItemScriptable> Items => items;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple instances of PlayerInventory in scene!");
            return;
        }
        Instance = this;
    }

    public void AddItem(ItemScriptable newItem)
    {
        items.Add(newItem);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemScriptable removingItem)
    {
        if (items.Remove(removingItem))
            OnInventoryChanged?.Invoke();
    }

    public bool HasItem(ItemScriptable item) => items.Contains(item);
}