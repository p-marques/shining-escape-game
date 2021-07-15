using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class UIButton : MonoBehaviour, IPointerEnterHandler,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
    IPointerExitHandler
{
    [Header("Background")]
    [SerializeField] private Image _targetBackground;
    [SerializeField] private Color _standardBackgroundColor = Color.white;
    [SerializeField] private Color _hoveredBackgroundColor = Color.gray;
    [SerializeField] private Color _pressedBackgroundColor = Color.black;

    [Header("Foreground")]
    [SerializeField] private TextMeshProUGUI _targetForeground;
    [SerializeField] private Color _standardForegroundColor = Color.white;
    [SerializeField] private Color _hoveredForegroundColor = Color.white;
    [SerializeField] private Color _pressedForegroundColor = Color.white;

    [Header("Events")]
    [SerializeField] private UnityEvent _onLeftClickEvent;
    [SerializeField] private UnityEvent _onRightClickEvent;
    [SerializeField] private UnityEvent _onHoverEnter;
    [SerializeField] private UnityEvent _onHoverExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            _onLeftClickEvent.Invoke();
        else
            _onRightClickEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _targetBackground.color = _pressedBackgroundColor;
        _targetForeground.color = _pressedForegroundColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _targetBackground.color = _hoveredBackgroundColor;
        _targetForeground.color = _hoveredForegroundColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetBackground.color = _hoveredBackgroundColor;
        _targetForeground.color = _hoveredForegroundColor;
        _onHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetBackground.color = _standardBackgroundColor;
        _targetForeground.color = _standardForegroundColor;
        _onHoverExit.Invoke();
    }
}
