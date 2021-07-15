using UnityEngine;

public class UIDeathScreen : MonoBehaviour
{
    [SerializeField] private SceneManagerEntity _sceneManager;
    [SerializeField] private GameObject _wrapper;
    [SerializeField] private VoidEventChannelSO _playerDiedEvent;

    private void OnEnable()
    {
        _playerDiedEvent.OnEventRaised += OnPlayerDied;
    }

    private void OnDisable()
    {
        _playerDiedEvent.OnEventRaised -= OnPlayerDied;
    }

    public void Retry()
    {
        _sceneManager.SpawnPlayer();
        _wrapper.SetActive(false);
    }

    public void OnPressExit()
    {
        Application.Quit();
    }

    private void OnPlayerDied()
    {
        _wrapper.SetActive(true);
    }
}
