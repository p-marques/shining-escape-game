using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Game/Input/Input Reader")]
public class InputReaderSO : ScriptableObject
{
    [SerializeField] private InputActionAsset _playerActions;

    private InputActionMap _gameplayActionMap;

    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction StartedRunningEvent = delegate { };
    public event UnityAction StoppedRunningEvent = delegate { };
    public event UnityAction StartedCrouchEvent = delegate { };
    public event UnityAction StoppedCrouchEvent = delegate { };

    private void OnEnable()
    {
        _gameplayActionMap = _playerActions.FindActionMap("GameplayActionMap");

        if (_gameplayActionMap == null)
            Debug.LogError("GameplayActionMap not found!");
        else
            EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartedRunningEvent.Invoke();
        }
        else if (context.canceled)
        {
            StoppedRunningEvent.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartedCrouchEvent.Invoke();
        }
        else if (context.canceled)
        {
            StoppedCrouchEvent.Invoke();
        }
    }

    public void EnableGameplayInput()
    {
        _gameplayActionMap.Enable();
    }

    public void DisableAllInput()
    {
        _gameplayActionMap.Disable();
    }
}
