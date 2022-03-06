using System;
using UnityEngine;


public class TempleteItem : ScriptableObject

{
    
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected ItemType _type;
}

[CreateAssetMenu(menuName = "Item/Defoult Item")]
public class AssetItem : TempleteItem, IItem
{
    public string Name => _name;

    public Sprite Icon => _icon;

    public ItemType Type => _type;
}



