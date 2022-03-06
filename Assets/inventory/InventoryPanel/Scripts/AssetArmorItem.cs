using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Armor Item")]
public class AssetArmorItem : TempleteItem, IArmorItem
{
    public string Name => _name;

    public Sprite Icon => _icon;

    public ItemType Type => _type;

    public ArmorType AType => _aType;

    [SerializeField] private ArmorType _aType;
}
