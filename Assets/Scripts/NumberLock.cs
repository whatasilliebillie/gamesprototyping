using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class NumberLock : MonoBehaviour
{
    private FocusInteractable focusInteractable;
    private Animator animator;

    [SerializeField] private LockPadInteractable[] keypads;

    [SerializeField] private int passcode;

    [SerializeField] private float waitForUnlock = 0.75f;

    public UnityEvent UnlockEvent;

    private void OnEnable()
    {
        foreach(LockPadInteractable keypad in keypads)
        {
            keypad.OnInteract += OnKeyPadInteract;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        focusInteractable = GetComponent<FocusInteractable>();
    }

    private void OnKeyPadInteract(int number)
    {
        float curPasscode = 0;

        for(int i = 0; i < keypads.Length; i++)
        {
            curPasscode += keypads[i].Number * Mathf.Pow(10, i + 1) / 10;
        }

        if(passcode == curPasscode)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        foreach(LockPadInteractable keypad in keypads)
        {
            keypad.ToggleInteraction(false);

            animator.SetBool("Locked", false);
        }

        StartCoroutine(UnlockTriggerWait());
    }

    private IEnumerator UnlockTriggerWait()
    {
        yield return new WaitForSeconds(waitForUnlock);

        focusInteractable.ToggleInteraction(false);
        focusInteractable.StopFocus();

        UnlockEvent?.Invoke();

        yield break;
    }

    private void OnDisable()
    {
        foreach (LockPadInteractable keypad in keypads)
        {
            keypad.OnInteract -= OnKeyPadInteract;
        }
    }
}
