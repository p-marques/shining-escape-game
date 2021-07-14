using TMPro;
using UnityEngine;

public class UIInteractionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _wrapper;
    [SerializeField] private IInteractableAnchorSO _currentAvailableInteractionAnchor;
    [SerializeField] private TextMeshProUGUI _interactionTextObject;

    private void Update()
    {
        if (_currentAvailableInteractionAnchor.IsSet && !_wrapper.activeInHierarchy)
        {
            _wrapper.SetActive(true);

            UpdateText();
        }
        else if (!_currentAvailableInteractionAnchor.IsSet)
        {
            _wrapper.SetActive(false);
        }
        else if (_interactionTextObject.text != _currentAvailableInteractionAnchor.Value.CallToAction)
        {
            UpdateText();
        }
    }

    private void UpdateText()
    {
        _interactionTextObject.text =
                $"[E] {_currentAvailableInteractionAnchor.Value?.CallToAction}";
    }
}
