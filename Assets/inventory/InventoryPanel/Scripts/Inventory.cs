using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    [SerializeField] private List<TempleteItem> Items;
    [SerializeField] private InventoryCell _inventoryCellTemplete;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _armoredParent;
    [SerializeField] private GameObject _player;

    public void OnEnable()
    {
        Render(Items);
    }

    public void Render(List<TempleteItem> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        int count = items.Count;

        items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplete, _container);
            cell.Init(_draggingParent, _armoredParent, _player.GetComponent<ChangeClothes>());
            cell.Render(item as IItem);
            cell.Injecting += () => Destroy(cell.gameObject);
        });

        
    }

}
