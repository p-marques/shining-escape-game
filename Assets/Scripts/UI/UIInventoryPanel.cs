using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    [SerializeField] private UIInventorySlot _slotPrefab;
    [SerializeField] private StorageSO _playerInventory;
    [SerializeField] private VoidEventChannelSO _onContentsUpdated;

    private UIInventorySlot[] _slots;

    private void Awake()
    {
        _slots = new UIInventorySlot[_playerInventory.Items.Count];

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, transform);
        }
    }

    private void OnEnable()
    {
        if (_onContentsUpdated)
            _onContentsUpdated.OnEventRaised += UpdateInventoryPanel;
    }

    private void OnDisable()
    {
        if (_onContentsUpdated)
            _onContentsUpdated.OnEventRaised -= UpdateInventoryPanel;
    }

    private void UpdateInventoryPanel()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Item = _playerInventory.Items[i];
        }
    }
}
