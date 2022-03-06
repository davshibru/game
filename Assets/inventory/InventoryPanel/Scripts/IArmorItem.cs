using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArmorType { Defauld, Head, Body, Bottom, Legs}

public interface IArmorItem : IItem
{

    public ArmorType AType { get; }
}
