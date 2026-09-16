using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Scriptable Objects/ItemScriptable")]
public class ItemScriptable : ScriptableObject
{
    public string DisplayName;

    public GameObject HeldItemPrefab;
}
