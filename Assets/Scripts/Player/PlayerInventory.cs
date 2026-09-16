using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [SerializeField] private List<ItemScriptable> items;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of PlayerInventory in scene!");
            return;
        }

        Instance = this;
    }

    public void AddItem(ItemScriptable newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(ItemScriptable removingItem)
    {
        if(items.Contains(removingItem))
        {
            items.Remove(removingItem);
        }
    }

    public bool HasItem(ItemScriptable item)
    {
        return items.Contains(item);
    }
}
