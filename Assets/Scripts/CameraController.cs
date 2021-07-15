using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _offset = new Vector2(100f, 300f);
    [Range(0.01f, 0.2f)]
    [SerializeField] private float _deltaFactor = 0.1f;

    [SerializeField] private TransformAnchorSO _playerTransformAnchor;

    private void Awake()
    {
        if (!_playerTransformAnchor || !_playerTransformAnchor.IsSet)
            Debug.LogError("CameraController has no target.");
    }

    private void FixedUpdate()
    {
        float valueX, valueY;

        if (!_playerTransformAnchor || !_playerTransformAnchor.IsSet) return;

        valueX = _playerTransformAnchor.Value.position.x + _offset.x * _playerTransformAnchor.Value.right.x;
        valueY = _playerTransformAnchor.Value.position.y + _offset.y;

        Vector3 newPos = new Vector3(valueX, valueY, transform.position.z);

        Vector3 delta = newPos - transform.position;

        transform.position += delta * _deltaFactor;
    }
}
