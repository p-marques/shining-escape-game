using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _offset = new Vector2(100f, 300f);
    [Range(0.01f, 0.2f)]
    [SerializeField] private float _deltaFactor = 0.1f;

    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerController>().transform;

        if (!_target) Debug.LogError("No player!");
    }

    private void FixedUpdate()
    {
        if (!_target) return;

        Vector3 newPos = new Vector3(_target.position.x + _offset.x * _target.right.x,
            _target.position.y + _offset.y, transform.position.z);

        Vector3 delta = newPos - transform.position;

        transform.position += delta * _deltaFactor;
    }
}
