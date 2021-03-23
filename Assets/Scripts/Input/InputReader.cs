using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _playerActions;

    private InputActionMap _gameplayActionMap;

    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction startedRunningEvent = delegate { };
    public event UnityAction stoppedRunningEvent = delegate { };
    public event UnityAction startedCrouchEvent = delegate { };
    public event UnityAction stoppedCrouchEvent = delegate { };

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
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        jumpEvent.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            startedRunningEvent.Invoke();
        }
        else if (context.canceled)
        {
            stoppedRunningEvent.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            startedCrouchEvent.Invoke();
        }
        else if (context.canceled)
        {
            stoppedCrouchEvent.Invoke();
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
