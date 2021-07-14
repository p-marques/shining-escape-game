using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDataSO _item;
    [SerializeField] private string _callToAction;
    [SerializeField] private ItemEventChannelSO _addItemEventChannel;
    
    public string CallToAction => _callToAction;

    public void Interact()
    {
        if (!_addItemEventChannel)
        {
            Debug.LogError("CollectableItem is missing event to add item");

            return;
        }

        _addItemEventChannel.RaiseEvent(_item);

        Destroy(gameObject);
    }
}
