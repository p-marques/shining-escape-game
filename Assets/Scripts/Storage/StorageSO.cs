using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageSO", menuName = "Game/Storage/New Storage")]
public class StorageSO : ScriptableObject
{
    [Range(1, 20)]
    [SerializeField] private short _size = 5;

    [SerializeField] private ItemDataSO[] _items;

    public IReadOnlyList<ItemDataSO> Items => _items;

    private void OnEnable()
    {
        _items = new ItemDataSO[_size];
    }

    public bool AddItem(ItemDataSO inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = inItem;

                return true;
            }
        }

        return false;
    }

    public void RemoveItem(ItemDataSO inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == inItem)
            {
                _items[i] = null;
            }
        }
    }

    public bool HasItem(ItemDataSO inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == inItem)
            {
                return true;
            }
        }

        return false;
    }
}
