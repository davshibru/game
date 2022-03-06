using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{

    // Ссылка на слот в инвентаре
    [SerializeField] private Transform armorSloats;
    // Массив с 3д моделями одежды
    [SerializeField] private List<ClothesController> clothesCon;



    public string pHead = "";
    public string pBody = "";
    public string pBottom = "";
    public string pLegs = "";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    /// <summary>
    /// Обновление одежды на персонаже
    /// </summary>
    /// <param name="type">Тип брони: голова, тело, низ или ноги</param>
    /// <param name="takeOn">Действие которое необходимо сделать, одеть или снять</param>
    /// <param name="name">Если одеть то необходимо название одежды</param>
    public void UpdateArmor(ArmorType type, bool takeOn, string name)
    {

        // Проверка на то что делать одевать или раздеть
        if (takeOn)
        {
            
            // Выключаются все меши брони с похожим типом
            TakeOffSameTypes(type);

            // Поиск определенной брони по имени и включение ее отображения
            foreach (var item in clothesCon)
            {
                if (item._nameOfModel.Equals(name))
                {
                    item._modelClothes.SetActive(true);
                }
            }
        }
        else
        {
            switch (type)
            {
                case ArmorType.Head:

                    // Выключаются все меши брони с типом тело
                    TakeOffSameTypes(type);

                    // Поиск брони по умолчанию и включение ее отображения
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("head"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                case ArmorType.Body:
                    // Выключаются все меши брони с типом тело
                    TakeOffSameTypes(type);

                    // Поиск брони по умолчанию и включение ее отображения
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("body"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                case ArmorType.Bottom:

                    // Выключаются все меши брони с типом тело
                    TakeOffSameTypes(type);

                    // Поиск брони по умолчанию и включение ее отображения
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("bottom"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }
                    break;
                case ArmorType.Legs:

                    // Выключаются все меши брони с типом тело
                    TakeOffSameTypes(type);

                    // Поиск брони по умолчанию и включение ее отображения
                    foreach (var item in clothesCon)
                    {
                        if (item._nameOfModel.Equals("legs"))
                        {
                            item._modelClothes.SetActive(true);
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        
    }

    private void TakeOffSameTypes(ArmorType type)
    {

        foreach (var item in clothesCon)
        {

            if (item._armorType == type)
            {
                item._modelClothes.SetActive(false);
            }

            
        }
    }



    // Класс для создания массива в котором хранятся ссылка на 3д модель одежды
    [System.Serializable]
    private class ClothesController
    {
        public GameObject _modelClothes;
        public string _nameOfModel;
        public ArmorType _armorType;

    }
}

