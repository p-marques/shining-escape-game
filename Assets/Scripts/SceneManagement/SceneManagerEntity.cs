using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerEntity : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private PlayerController _playerPrefab;
    [SerializeField] private Transform _levelStartingPoint;

    private PlayerController _playerController;

    private void Awake()
    {
        if (!_camera || !_playerPrefab || !_levelStartingPoint)
        {
            Debug.LogError("SceneManager is missing necessary references.");

            return;
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (_playerController)
        {
            Debug.LogError("SceneManagerEntity.SpawnPlayer() called but player was already instanced.");

            return;
        }

        _playerController = Instantiate(_playerPrefab,
            _levelStartingPoint.position, _levelStartingPoint.rotation);
    }
}
