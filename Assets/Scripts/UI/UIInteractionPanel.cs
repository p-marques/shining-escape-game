using TMPro;
using UnityEngine;

public class UIInteractionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _wrapper;
    [SerializeField] private IInteractableAnchorSO _currentAvailableInteractionAnchor;
    [SerializeField] private TextMeshProUGUI _interactionTextObject;

    private void Update()
    {
        if (_currentAvailableInteractionAnchor.IsSet)
        {
            if (!_wrapper.activeInHierarchy || 
                _interactionTextObject.text != _currentAvailableInteractionAnchor.Value.CallToAction)
            {
                _wrapper.SetActive(true);

                UpdateText();
            }
        }
        else if (_wrapper.activeInHierarchy)
        {
            _wrapper.SetActive(false);
        }
    }

    private void UpdateText()
    {
        _interactionTextObject.text =
                $"[E] {_currentAvailableInteractionAnchor.Value?.CallToAction}";
    }
}
