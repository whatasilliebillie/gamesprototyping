using UnityEngine;
using TMPro;

public class LeverPuzzleHandler : MonoBehaviour
{
    [SerializeField] private FocusInteractable puzzleFocusInteractable;
    [SerializeField] private EventBridge completeEventBridge;

    [SerializeField] private Animator[] leverAnimators;
    private int[] _leverArray;

    private bool _isComplete;

    private void Start()
    {
        _leverArray = new int[leverAnimators.Length];

        SetLever(0, 3);
        SetLever(1, 3);
        SetLever(2, 3);
        SetLever(3, 3);
        SetLever(4, 3);
    }

    public void ToggleLever(int leverNumber)
    {
        SetLever(leverNumber, NextNum(_leverArray[leverNumber], -1));

        int rightLeverNum = leverNumber + 1;

        if(rightLeverNum < _leverArray.Length)
        {
            if(_leverArray[rightLeverNum] != 4)
            {
                SetLever(rightLeverNum, NextNum(_leverArray[rightLeverNum], 1));
            }
            
        }

        int leftLeverNum = leverNumber - 1;

        if (leftLeverNum >= 0)
        {
            if(_leverArray[leftLeverNum] != 4)
            {
                SetLever(leftLeverNum, NextNum(_leverArray[leftLeverNum], 1));
            }
        }

        CheckComplete();
    }

    private void CheckComplete()
    {
        for (int i = 0; i < _leverArray.Length; i++)
        {
            if (_leverArray[i] != 1) return;
        }

        puzzleFocusInteractable.ToggleInteraction(false);
        PlayerUIHandler.Instance.StopFocus();

        completeEventBridge.InvokeBridgingEvent();

        _isComplete = true;
    }

    public void ResetLevers()
    {
        for(int i = 0; i < _leverArray.Length; i++)
        {
            SetLever(i, 3);
        }
    }

    private int NextNum(int curNum, int change)
    {
        int nextNum = curNum + change;

        if(nextNum > 3)
        {
            nextNum = 1;
        }

        if(nextNum <= 0)
        {
            nextNum = 3;
        }

        return nextNum;
    }

    private void SetLever(int leverNum, int value)
    {
        _leverArray[leverNum] = value;

        leverAnimators[leverNum].SetInteger("Position", value);
    }
}

/*
        SetLever(leverNumber, NextNum(_leverArray[leverNumber], -1));

        int rightLeverNum = leverNumber + 1;

        if (rightLeverNum >= _leverArray.Length)
        {
            rightLeverNum -= _leverArray.Length;
        }

        if(_leverArray[rightLeverNum] != 2)
        {
            SetLever(rightLeverNum, NextNum(_leverArray[rightLeverNum], -1));
        }

        int leftLeverNum = leverNumber - 1;

        if (leftLeverNum < 0)
        {
            leftLeverNum += _leverArray.Length;
        }

        if(_leverArray[leftLeverNum] != 2)
        {
            SetLever(leftLeverNum, NextNum(_leverArray[leftLeverNum], -1));
        }
        */
