using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image slotPrefab;      // a prefab with an Image component
    [SerializeField] private int slotCount = 5;

    private Image[] slots;

    private void Start()
    {
        // Build empty item slots for us to fill
        slots = new Image[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = Instantiate(slotPrefab, transform);

        // Subscribe in Start so PlayerInventory.Instance (set in Awake) exists
        PlayerInventory.Instance.OnInventoryChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        var items = PlayerInventory.Instance.Items;

        for (int i = 0; i < slots.Length; i++)
        {
            // The slot's child "Icon" image shows the item; the slot itself is the background
            Image icon = slots[i].transform.GetChild(0).GetComponent<Image>();

            if (i < items.Count && items[i] != null)
            {
                icon.sprite = items[i].Icon;
                icon.enabled = true;
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
            }
        }
    }
}