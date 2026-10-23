using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class PadlockHandler : MonoBehaviour
{
    private FocusInteractable focusInteractable;
    private Animator animator;

    [SerializeField] private PadlockHandler linkedPadlock;

    [SerializeField] private DialInteractable[] dials;

    [SerializeField] private string passcode;

    [SerializeField] private float waitForUnlock = 0.75f;

    public UnityEvent UnlockEvent;

    private void OnEnable()
    {
        foreach(DialInteractable keypad in dials)
        {
            keypad.OnInteract += OnKeyPadInteract;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        focusInteractable = GetComponent<FocusInteractable>();
    }

    private void OnKeyPadInteract()
    {
        string curPass = "";

        for(int i = 0; i < dials.Length; i++)
        {
            curPass += dials[i].CurNumber.ToString();
        }

        Debug.Log(curPass);

        if (passcode == curPass)
        {
            Unlock();
        }

        /*
        float curPasscode = 0;

        for(int i = 0; i < dials.Length; i++)
        {
            curPasscode += dials[i].CurNumber * Mathf.Pow(10, i + 1) / 10;
        }

        if(passcode == curPasscode)
        {
            Unlock();
        }
        */
    }

    public void Unlock()
    {
        foreach(DialInteractable keypad in dials)
        {
            keypad.ToggleInteraction(false);
        }

        if(linkedPadlock != null)
        {
            linkedPadlock.Unlock();
        }

        animator.SetTrigger("Toggle");

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
        foreach (DialInteractable keypad in dials)
        {
            keypad.OnInteract -= OnKeyPadInteract;
        }
    }
}
