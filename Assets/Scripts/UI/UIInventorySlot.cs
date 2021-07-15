using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private ItemDataSO _item;

    public ItemDataSO Item 
    {
        get => _item;
        set
        {
            _item = value;

            if (value)
            {
                _icon.sprite = value.Sprite;
                _icon.color = Color.white;
            }
            else
            {
                _icon.color = Color.clear;
            }
        }
    }
}
