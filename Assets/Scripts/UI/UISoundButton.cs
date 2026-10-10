using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class UISoundButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private SoundSO hoverSound;
    [SerializeField] private SoundSO clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsUsable()) return;
        SoundManager.Instance.PlayUISound(hoverSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsUsable()) return;
        SoundManager.Instance.PlayUISound(clickSound);
    }

    private bool IsUsable()
    {
        if (SoundManager.Instance == null) return false;

        Selectable selectable = GetComponent<Selectable>();
        return selectable != null && selectable.interactable;
    }
}