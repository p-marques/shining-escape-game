using System.Collections;
using UnityEngine;

public class JackSpawner : MonoBehaviour
{
    [SerializeField] private JackController _jackPrefab;

    private BoxCollider2D _collider;
    private Animator _animator;
    private bool _canSpawn;
    private WaitUntil _waitCondition;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _waitCondition = new WaitUntil(() => _canSpawn);
    }

    private IEnumerator SpawnCR()
    {
        _canSpawn = false;
        _animator.SetTrigger("Destroy");

        yield return _waitCondition;

        Instantiate(_jackPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            _collider.enabled = false;
            StartCoroutine(SpawnCR());
        }
    }

    public void DestructionAnimFinished()
    {
        _canSpawn = true;
    }
}
