using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Game/Storage/New Item")]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;

    public string ItemName => _itemName;
    public Sprite Sprite => _sprite;
}
