using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public enum ItemType { Defoult, Food, Weapon, Armor}

public interface IItem
{
    string Name { get; }
    Sprite Icon { get; }
    ItemType Type { get; }
}


