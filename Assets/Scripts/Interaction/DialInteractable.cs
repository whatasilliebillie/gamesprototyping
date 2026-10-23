using UnityEngine;
using System;
using TMPro;

public class DialInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;

    public int CurNumber => _curNumber;
    private int _curNumber;

    public bool InteractionEnabled => _interactionEnabled;
    private bool _interactionEnabled = true;

    public Action OnInteract;

    public HoverIcon CurHoverIcon => GetHoverIcon();

    private HoverIcon GetHoverIcon()
    {
        return _interactionEnabled ? HoverIcon.Circle : HoverIcon.Default;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!_interactionEnabled) return;

        _curNumber++;

        if(_curNumber >= 10)
        {
            _curNumber = 0;
        }

        UpdateDialAnimator();

        OnInteract?.Invoke();
    }

    private void UpdateDialAnimator()
    {
        animator.SetFloat("DialPos", _curNumber / 9f);
    }

    public void ToggleInteraction(bool toggle)
    {
        _interactionEnabled = toggle;
    }
}
