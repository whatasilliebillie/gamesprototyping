using UnityEngine;
using System;
using TMPro;

public class LockPadInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_Text numberText;
    private MeshRenderer meshRenderer;

    [SerializeField] private Material standardMaterial;
    [SerializeField] private Material highlightMaterial;

    public int Number => _setNumber;

    private int _setNumber;

    private bool _isInteractable = true;

    public Action<int> OnInteract;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetHover(bool toggle)
    {
        if(_isInteractable)
        {
            meshRenderer.material = toggle ? highlightMaterial : standardMaterial;
            numberText.color = toggle ? Color.black : Color.white;
        }
    }

    public void Interact()
    {
        if (!_isInteractable) return;

        _setNumber++;

        if(_setNumber >= 10)
        {
            _setNumber = 0;
        }

        numberText.text = _setNumber.ToString();

        OnInteract?.Invoke(_setNumber);
    }

    public void ToggleInteraction(bool toggle)
    {
        _isInteractable = toggle;

        if(!_isInteractable)
        {
            meshRenderer.material = standardMaterial;
            numberText.color = Color.white;
        }
    }
}
