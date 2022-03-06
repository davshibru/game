using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // Событие выброса элемента из инвентаря
    public event Action Injecting;

    [HideInInspector]
    public string _name;
    [SerializeField] private GameObject _emptyArmorSloat;
    [SerializeField] private Text _nameField;
    [SerializeField] private Image _iconField;
    private ItemType _type;
    private ChangeClothes _changeClothes;

    // for armor
    private ArmorType _aType;


    private Transform _draggingParent; // ссылка на базавый объект инвентаря
    private Transform _originalParent; // ссылка на радительский объект ячейки
    private Transform _armoredParent; // ссылка на радительский объект ячеек для брони



    private Transform tempItem;

    private string DragStatus = "";

    public void Init(Transform draggingParent, Transform armoredParent, ChangeClothes changeClothes)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
        _armoredParent = armoredParent;
        _changeClothes = changeClothes;
    }

    public void Render(IItem item)
    {
        _name = item.Name;
        _nameField.text = item.Name;
        _iconField.sprite = item.Icon;
        _type = item.Type;

        if (item is IArmorItem)
        {
            var x = item as IArmorItem;
            _aType = x.AType;
        }

    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_type != ItemType.Defoult)
        {
           
            transform.parent = _draggingParent;

            if (In((RectTransform)_originalParent))
            {
                DragStatus = "inventory";
            }
            else if (In((RectTransform)_armoredParent))
            {
                DragStatus = "armor";
            }
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (_type != ItemType.Defoult)
        {
            transform.position = Input.mousePosition;
        }
    }
    // Запуск? события
    private void Inject()
    {
        Injecting?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверка на то откуда берется ячейка из инвентаря или из брони
        switch (DragStatus)
        {
            // из инветаря
            case "inventory":

                if (In((RectTransform)_originalParent))
                {
                    InserdInGrid();
                }
                else if (In((RectTransform)_armoredParent))
                {
                    InserdInArmorGrid();
                }
                else
                {
                    Inject();
                }
                break;
            // из брони
            case "armor":

                // из брони в инвентарь
                if (In((RectTransform)_draggingParent))
                {


                    // Поставить в оставшуюся ячейку пустой элемент
                    var head = _armoredParent.GetChild(0);
                    var body = _armoredParent.GetChild(1);
                    var bottom = _armoredParent.GetChild(2);
                    var legs = _armoredParent.GetChild(3);

                    switch (_aType)
                    {
                        
                        case ArmorType.Head:
                            head.GetComponent<ArmorItem>().isEmpty = true;
                            var emptyHead = Instantiate(_emptyArmorSloat, head);
                            _changeClothes.UpdateArmor(ArmorType.Head, false, "");
                            break;
                        case ArmorType.Body:
                            body.GetComponent<ArmorItem>().isEmpty = true;
                            var emptyBody = Instantiate(_emptyArmorSloat, body);
                            _changeClothes.UpdateArmor(ArmorType.Body, false, "");
                            break;
                        case ArmorType.Bottom:
                            bottom.GetComponent<ArmorItem>().isEmpty = true;
                            var emptyBottom = Instantiate(_emptyArmorSloat, bottom);
                            _changeClothes.UpdateArmor(ArmorType.Bottom, false, "");
                            break;
                        case ArmorType.Legs:
                            legs.GetComponent<ArmorItem>().isEmpty = true;
                            var emptyLegs = Instantiate(_emptyArmorSloat, legs);
                            _changeClothes.UpdateArmor(ArmorType.Legs, false, "");
                            break;
                        default:
                            break;
                    }

                    // Засунуть ячейку в инветарь
                    InserdInGrid();
                }
                else
                {
                    Inject();
                }
                break;
        }

        
        

        DragStatus = "";

    }

    private void InserdInGrid()
    {
        if (_type != ItemType.Defoult)
        {

            int closestIndex = 0;
            for (int i = 0; i < _originalParent.transform.childCount; i++)
            {
                if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                    Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
                {
                    closestIndex = i;
                }
            }


            

            transform.parent = _originalParent;
            transform.SetSiblingIndex(closestIndex);

            
        }
    }

    public void InserdInArmorGrid()
    {
        if (_type == ItemType.Armor)
        {

            var head = _armoredParent.GetChild(0);
            var body = _armoredParent.GetChild(1);
            var bottom = _armoredParent.GetChild(2);
            var legs = _armoredParent.GetChild(3);

            switch (_aType)
            {
                
                case ArmorType.Head:

                    setArmorOn(head);

                    break;
                case ArmorType.Body:

                    setArmorOn(body);

                    break;
                case ArmorType.Bottom:

                    setArmorOn(bottom);

                    break;
                case ArmorType.Legs:

                    setArmorOn(legs);

                    break;
                default:
                    InserdInGrid();
                    break;
            }
        }
        else
        {
            InserdInGrid();
        }
    }


    private void setArmorOn(Transform partOfBody)
    {
        // Если ячейка перенесена на достаточно близкое растояние и элемент брони соответствует месту на которое его хотят надеть
        if (Vector3.Distance(transform.position, partOfBody.position) < 20 && _aType == partOfBody.GetComponent<ArmorItem>()._aType)
        {

            if (partOfBody.GetComponent<ArmorItem>().isEmpty)
            {
                Destroy(partOfBody.GetChild(0).gameObject);
                transform.parent = partOfBody;
                transform.SetAsFirstSibling();
                partOfBody.GetComponent<ArmorItem>().isEmpty = false;
            }
            else
            {
                partOfBody.GetChild(0).parent = _originalParent;
                transform.parent = partOfBody;
                transform.SetAsFirstSibling();
            }

            _changeClothes.UpdateArmor(_aType, true, _name);
        }
        else
        { 
            InserdInGrid();
        }
    }


    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }


}

