using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private StorageSO _inventory;

    [Header("Events")]
    [SerializeField] private ItemEventChannelSO _addItemEventChannel;
    [SerializeField] private VoidEventChannelSO _onContentsUpdated;

    private void OnEnable()
    {
        _addItemEventChannel.OnEventRaised += AddItem;
    }

    private void OnDisable()
    {
        _addItemEventChannel.OnEventRaised -= AddItem;
    }

    private void AddItem(ItemDataSO item)
    {
        if (!_inventory)
        {
            Debug.Log("PlayerInventoryManager is missing the inventory SO.");

            return;
        }

        _inventory.AddItem(item);

        if (_onContentsUpdated)
            _onContentsUpdated.RaiseEvent();
    }
}
